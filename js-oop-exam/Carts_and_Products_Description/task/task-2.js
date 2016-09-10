/* globals module */

"use strict";

function solve() {

    function* Ids() {
        let next = 0;
        while (true) {
            yield next += 1;
        }
    }

    function validateProduct(product) {
        const isProduct = product instanceof Product;
        const isProductLike = product.name && product.productType && product.price;

        if (!(isProduct || isProductLike)) {
            throw new Error();
        }
    }

    class Product {
        /* .... */
        constructor(productType, name, price) {
            this.productType = productType;
            this.name = name;
            this.price = price;
        }
    }

    class ShoppingCart {
        /* .... */
        constructor() {
            this.products = [];
        }

        add(product) {
            validateProduct(product);
            this.products.push(product);
            return this;
        }

        remove(product) {
            validateProduct(product);

            if (this.products.length === 0) {
                throw new Error();
            }

            const indexToRemove = this.products.findIndex(item => {
                const names = item.name === product.name;
                const type = item.productType === product.productType;
                const prices = item.price === product.price;

                return names && type && prices;
            });

            if (indexToRemove < 0) {
                throw new Error();
            }

            this.products.splice(indexToRemove, 1);
            return this;
        }

        showCost() {
            let sum = 0;

            if (this.products.length === 0) {
                return sum;
            }

            for (const item of this.products) {
                sum += item.price;
            }

            return sum;
        }

        showProductTypes() {
            const types = [];

            if (this.products.length === 0) {
                return types;
            }

            for (const item of this.products) {
                if (types.indexOf(item.productType) < 0) {
                    types.push(item.productType);
                }
            }

            // Sort ignore case ?
            types.sort();

            return types;
        }

        getInfo() {
            const info = {
                products: [],
                totalPrice: 0
            };

            if (this.products.length === 0) {
                return info;
            }

            this.products.forEach(item => {
                const indexOfElement = info.products.findIndex(el => el.name === item.name);
                if (indexOfElement < 0) {
                    info.products.push({
                        name: item.name,
                        quantity: 1,
                        totalcost: item.price
                    });
                } else {
                    info.products[indexOfElement].quantity += 1;
                    info.products[indexOfElement].totalcost += item.price;
                }
            });

            info.products.forEach(pr => {
                info.totalPrice += pr.totalcost;
            });

            return info;
        }
    }

    return {
        Product,
        ShoppingCart
    };
}

module.exports = solve;