function solve() {
    'use strict';

    const ERROR_MESSAGES = {
        INVALID_NAME_TYPE: 'Name must be string!',
        INVALID_NAME_LENGTH: 'Name must be between between 2 and 20 symbols long!',
        INVALID_NAME_SYMBOLS: 'Name can contain only latin symbols and whitespaces!',
        INVALID_MANA: 'Mana must be a positive integer number!',
        INVALID_EFFECT: 'Effect must be a function with 1 parameter!',
        INVALID_DAMAGE: 'Damage must be a positive number that is at most 100!',
        INVALID_HEALTH: 'Health must be a positive number that is at most 200!',
        INVALID_SPEED: 'Speed must be a positive number that is at most 100!',
        INVALID_COUNT: 'Count must be a positive integer number!',
        INVALID_SPELL_OBJECT: 'Passed objects must be Spell-like objects!',
        NOT_ENOUGH_MANA: 'Not enough mana!',
        TARGET_NOT_FOUND: 'Target not found!',
        INVALID_BATTLE_PARTICIPANT: 'Battle participants must be ArmyUnit-like!',
        INVALID_ALIGNMENT: 'Alignment must be good, neutral or evil!'
    };

    // your implementation goes here

    const SPELL_NAME_PATTERN = /^[A-Za-z\s]*$/;

    const validator = (() => {
        const SPELL_NAME_PATTERN = /^[A-Za-z\s]*$/;
        return {
            validateName(value) {
                if (typeof value !== 'string') {
                    throw new Error(ERROR_MESSAGES.INVALID_NAME_TYPE);
                }

                if (!SPELL_NAME_PATTERN.test(value)) {
                    throw new Error(ERROR_MESSAGES.INVALID_NAME_SYMBOLS);
                }

                if (!(2 <= value.length && value.length <= 20)) {
                    throw new Error(ERROR_MESSAGES.INVALID_NAME_LENGTH);
                }
            }
        }
    })();

    function* IdGenerator() {
        let last = 0;
        while (true) {
            yield last += 1;
        }
    }

    class Spell {
        constructor(name, manaCost, effect) {
            this.name = name;
            this.manaCost = manaCost;
            this.effect = effect;
        }

        get name() {
            return this._name;
        }

        set name(value) {
            // if (typeof value !== 'string') {
            //     throw new Error(INVALID_NAME_TYPE);
            // }

            // if (!SPELL_NAME_PATTERN.test(value)) {
            //     throw new Error(INVALID_NAME_SYMBOLS);
            // }

            // if (!(2 <= value.length && value.length <= 20)) {
            //     throw new Error(INVALID_NAME_LENGTH);
            // }
            validator.validateName(value);

            this._name = value;
        }

        get manaCost() {
            return this._manaCost;
        }

        set manaCost(value) {
            value = Number(value);
            if (isNaN(value)) {
                throw new Error(ERROR_MESSAGES.INVALID_MANA);
            }

            if (value <= 0) {
                throw new Error(ERROR_MESSAGES.INVALID_MANA);
            }

            if ((value | 0) !== value) {
                throw new Error(ERROR_MESSAGES.INVALID_MANA);
            }

            this._manaCost = value;
        }

        get effect() {
            return this._effect;
        }

        set effect(value) {
            if (value.length !== 1) {
                throw new Error(ERROR_MESSAGES.INVALID_EFFECT);
            }
            // !!!
            if (typeof value !== 'function') {
                throw new Error(ERROR_MESSAGES.INVALID_EFFECT);
            }

            this._effect = value;
        }
    }

    class Unit {
        constructor(name, alignment) {
            this.name = name;
            this.alignment = alignment;
        }

        get name() {
            return this._name;
        }

        set name(value) {
            validator.validateName(value);

            this._name = value;
        }

        get alignment() {
            return this._alignment;
        }

        set alignment(value) {
            const isGood = value === 'good';
            const isNeutral = value === 'neutral';
            const isEvil = value === 'evil';

            if (!isGood && !isNeutral && !isEvil) {
                throw new Error(ERROR_MESSAGES.INVALID_ALIGNMENT);
            }

            this._alignment = value;
        }
    }

    const armyUnitIdGeneraor = IdGenerator();
    class ArmyUnit extends Unit {
        constructor(name, alignment, damage, health, count, speed) {
            super(name, alignment);
            this.id = armyUnitIdGeneraor.next().value;

            this.damage = damage;
            this.health = health;
            this.count = count;
            this.speed = speed;
        }

        get damage() {
            return this._damage;
        }

        set damage(value) {
            value = Number(value);
            if (isNaN(value)) {
                throw new Error(ERROR_MESSAGES.INVALID_DAMAGE);
            }

            if (value <= 0) {
                throw new Error(ERROR_MESSAGES.INVALID_DAMAGE);
            }

            if (100 < value) {
                throw new Error(ERROR_MESSAGES.INVALID_DAMAGE);
            }

            this._damage = value;
        }

        get health() {
            return this._health;
        }

        set health(value) {
            value = Number(value);
            if (isNaN(value)) {
                throw new Error(ERROR_MESSAGES.INVALID_HEALTH);
            }

            if (value < 0) {
                throw new Error(ERROR_MESSAGES.INVALID_HEALTH);
            }

            if (200 < value) {
                throw new Error(ERROR_MESSAGES.INVALID_HEALTH);
            }

            this._health = value;
        }

        get count() {
            return this._count;
        }

        set count(value) {
            value = Number(value);
            if (isNaN(value)) {
                throw new Error(ERROR_MESSAGES.INVALID_COUNT);
            }

            if (value < 0) {
                throw new Error(ERROR_MESSAGES.INVALID_COUNT);
            }

            if ((value | 0) !== value) {
                throw new Error(ERROR_MESSAGES.INVALID_COUNT);
            }

            this._count = value;
        }

        get speed() {
            return this._speed;
        }

        set speed(value) {
            value = Number(value);
            if (isNaN(value)) {
                throw new Error(ERROR_MESSAGES.INVALID_SPEED);
            }

            if (value < 0) {
                throw new Error(ERROR_MESSAGES.INVALID_SPEED);
            }

            if (100 < value) {
                throw new Error(ERROR_MESSAGES.INVALID_SPEED);
            }

            this._speed = value;
        }
    }

    class Commander extends Unit {
        constructor(name, alignment, mana, spellbook, army) {
            super(name, alignment);

            this.mana = mana;
            this.spellbook = spellbook || [];
            this.army = army || [];
        }

        get mana() {
            return this._mana;
        }

        set mana(value) {
            value = Number(value);
            if (isNaN(value)) {
                throw new Error(ERROR_MESSAGES.INVALID_MANA);
            }

            if (value <= 0) {
                throw new Error(ERROR_MESSAGES.INVALID_MANA);
            }

            if ((value | 0) !== value) {
                throw new Error(ERROR_MESSAGES.INVALID_MANA);
            }

            this._mana = value;
        }
    }

    const battlemanager = {
        commanders: [],
        getCommander(name, alignment, mana) {
            return new Commander(name, alignment, mana);
        },
        getArmyUnit(options) {
            return new ArmyUnit(
                options.name,
                options.alignment,
                options.damage,
                options.health,
                options.count,
                options.speed);
        },
        getSpell(name, manaCost, effect) {
            return new Spell(name, manaCost, effect);
        },
        addCommanders(...input) {
            battlemanager.commanders.push(...input);
            return this;
        },
        addArmyUnitTo(commanderName, armyUnit) {
            const commander = battlemanager.commanders.find(item => {
                return item.name === commanderName;
            });

            commander.army.push(armyUnit);
            return this;
        },
        addSpellsTo(commanderName, ...spells) {
            const commander = battlemanager.commanders.find(item => {
                return item.name === commanderName;
            });

            spells.forEach(item => {
                validateSpell(item);
                // if (!item.name || !item.manaCost || !item.effect) {
                //     throw new Error("Passed objects must be Spell-like objects!");
                // }

                // try {
                //     new Spell(item.name, item.manaCost, item.effect);
                // } catch (e) {
                //     throw new Error("Passed objects must be Spell-like objects!");
                // }

            });

            commander.spellbook.push(...spells);

            function validateSpell(spell) {
                if (spell === undefined) {
                    throw new Error(ERROR_MESSAGES.INVALID_SPELL_OBJECT);
                }

                if (spell === null) {
                    throw new Error(ERROR_MESSAGES.INVALID_SPELL_OBJECT);
                }

                let value = spell.name;
                if (typeof value !== 'string') {
                    throw new Error(ERROR_MESSAGES.INVALID_SPELL_OBJECT);
                }

                if (!SPELL_NAME_PATTERN.test(value)) {
                    throw new Error(ERROR_MESSAGES.INVALID_SPELL_OBJECT);
                }

                if (!(2 <= value.length && value.length <= 20)) {
                    throw new Error(ERROR_MESSAGES.INVALID_SPELL_OBJECT);
                }

                value = Number(spell.manaCost);
                if (isNaN(value)) {
                    throw new Error(ERROR_MESSAGES.INVALID_SPELL_OBJECT);
                }

                if (value <= 0) {
                    throw new Error(ERROR_MESSAGES.INVALID_SPELL_OBJECT);
                }

                if (Math.floor(value) !== value) {
                    throw new Error(ERROR_MESSAGES.INVALID_SPELL_OBJECT);
                }

                value = spell.effect;
                if (typeof value !== 'function') {
                    throw new Error(ERROR_MESSAGES.INVALID_SPELL_OBJECT);
                }

                if (value.length !== 1) {
                    throw new Error(ERROR_MESSAGES.INVALID_SPELL_OBJECT);
                }
                // !!!

            }

            return this;
        },
        findCommanders(query) {
            const matches = battlemanager.commanders.filter(item => {
                if (query.name && item.name !== query.name) {
                    return false;
                }

                if (query.alignment && item.alignment !== query.alignment) {
                    return false;
                }

                return true;
            });

            matches.sort();

            return matches;
        },
        findArmyUnitById(id) {
            let unit;
            battlemanager.commanders.forEach(comm => {
                comm.army.forEach(u => {
                    if (u.id === id) {
                        unit = u;
                    }
                });
            });

            return unit;
        },
        findArmyUnits(query) {
            let units = [];

            battlemanager.commanders.forEach(comm => {
                comm.army.forEach(item => {
                    if (query.id && item.id !== query.name) {
                        return false;
                    }

                    if (query.name && item.name !== query.name) {
                        return false;
                    }

                    if (query.alignment && item.alignment !== query.alignment) {
                        return false;
                    }

                    units.push(item);
                });
            });

            units.sort((a, b) => {
                const compareSpeed = b.speed - a.speed;
                if (compareSpeed !== 0) {
                    return compareSpeed;
                }

                return a.name > b.name ? 1 : a.name < b.name ? -1 : 0;
            });

            return units;
        },
        spellcast(casterName, spellName, targetUnitId) {
            const commander = battlemanager.commanders.find(comm => comm.name === casterName);
            if (!commander) {
                throw new Error('Cannot cast with non-existant commander ' + casterName + '!');
            }

            const spell = commander.spellbook.find(spel => spel.name === spellName);
            if (!spell) {
                throw new Error(casterName + ' does not know ' + spellName);
            }

            targetUnitId = Number(targetUnitId);
            if (isNaN(targetUnitId)) {
                throw new Error('Target not found!');
            }

            let unit;
            battlemanager.commanders.forEach(comm => {
                comm.army.forEach(u => {
                    if (u.id === targetUnitId) {
                        unit = u;
                    }
                });
            });

            if (!unit) {
                throw new Error('Target not found!');
            }

            if (commander.mana < spell.manaCost) {
                throw new Error('Not enough mana!');
            }
// [].forEach.call()
            spell.effect.call(unit, unit);
            commander.mana -= spell.manaCost;
            return this;
        },
        battle(attacker, defender) {
            try {
                validateUnit(attacker);
                validateUnit(defender);
            } catch (e) {
                throw new Error('Battle participants must be ArmyUnit-like!');
            }

            const attackerTotalDamage = attacker.count * attacker.damage;
            const defenterTotalHealth = defender.count * defender.health;

            const defenderHealthRemaining = defenterTotalHealth - attackerTotalDamage;
            let newCount = Math.ceil(defenderHealthRemaining / defender.health);

            if (newCount < 0) {
                defender.count = 0;
            } else {
                defender.count = newCount;
            }

            function validateUnit(unit) {
                let value = Number(unit.damage);
                if (isNaN(value)) {
                    throw new Error(ERROR_MESSAGES.INVALID_DAMAGE);
                }

                if (value <= 0) {
                    throw new Error(ERROR_MESSAGES.INVALID_DAMAGE);
                }

                if (100 < value) {
                    throw new Error(ERROR_MESSAGES.INVALID_DAMAGE);
                }

                value = Number(unit.health);
                if (isNaN(value)) {
                    throw new Error(ERROR_MESSAGES.INVALID_HEALTH);
                }

                if (value < 0) {
                    throw new Error(ERROR_MESSAGES.INVALID_HEALTH);
                }

                if (200 < value) {
                    throw new Error(ERROR_MESSAGES.INVALID_HEALTH);
                }

                value = Number(unit.count);
                if (isNaN(value)) {
                    throw new Error(ERROR_MESSAGES.INVALID_COUNT);
                }

                if (value < 0) {
                    throw new Error(ERROR_MESSAGES.INVALID_COUNT);
                }

                if ((value | 0) !== value) {
                    throw new Error(ERROR_MESSAGES.INVALID_COUNT);
                }
            }

            return this;
        }
    };

    return battlemanager;
}

module.exports = solve;