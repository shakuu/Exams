﻿using System;
using System.Collections.Generic;
using System.Linq;
using FastAndFurious.ConsoleApplication.Common.Constants;
using FastAndFurious.ConsoleApplication.Contracts;
using FastAndFurious.ConsoleApplication.Common.Utils;

namespace FastAndFurious.ConsoleApplication.Models.Tracks.Abstract
{
    public class RaceTrack : IRaceTrack
    {
        private readonly string trackName;
        private readonly int maxParticipantsCount;
        private readonly int minParticipantsCount;
        private readonly int trackLengthInMeters;
        private readonly ICollection<IDriver> participants;
        private readonly ICollection<ICollection<TimeSpan>> finishedRacesResults;

        public RaceTrack(
            string trackName,
            int maxParticipantsCount,
            int minParticipantsCount,
            int trackLengthInMeters)
        {
            this.trackName = trackName;
            this.maxParticipantsCount = maxParticipantsCount;
            this.minParticipantsCount = minParticipantsCount;
            this.trackLengthInMeters = trackLengthInMeters;
            this.participants = new List<IDriver>();
            this.finishedRacesResults = new List<ICollection<TimeSpan>>();

            this.Id = DataGenerator.GenerateId();
        }

        public int MaxParticipantsCount
        {
            get
            {
                return this.maxParticipantsCount;
            }
        }
        public int MinParticipantsCount
        {
            get
            {
                return this.minParticipantsCount;
            }
        }
        public int TrackLengthInMeters
        {
            get
            {
                return this.trackLengthInMeters;
            }
        }
        public string TrackName
        {
            get
            {
                return this.trackName;
            }
        }
        public IEnumerable<IDriver> Participants
        {
            get
            {
                return this.participants;
            }
        }
        public IEnumerable<IEnumerable<TimeSpan>> FinishedRacesResults
        {
            get
            {
                return this.finishedRacesResults;
            }
        }

        public int Id
        {
            get;
            set;
        }

        public void AddParticipant(IDriver participant)
        {
            if (this.participants.Count < this.MaxParticipantsCount)
            {
                this.participants.Add(participant);
            }
        }
        public bool RemoveParticipant(IDriver participant)
        {
            return this.participants.Remove(participant);
        }
        public void RunRace()
        {
            var participantsCount = this.participants.Count();

            if (this.MinParticipantsCount <= participantsCount)
            {
                // TODO: STIMPL
                var raceResults = new List<TimeSpan>(participantsCount);
                foreach (var participant in this.Participants)
                {
                    var timeRequiredToFinishTheTrack = participant.ActiveVehicle.Race(this.TrackLengthInMeters);
                    raceResults.Add(timeRequiredToFinishTheTrack);
                }

                this.finishedRacesResults.Add(raceResults);
                this.participants.Clear();
            }
            else
            {
                Console.WriteLine(String.Format(GlobalConstants.CannotStartRaceMessage, participantsCount));
            }
        }
    }
}
