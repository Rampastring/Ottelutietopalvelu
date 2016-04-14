﻿// @author Rami Pasanen
// @version 14.04.2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace WebApplication
{
    /// <summary>
    /// Otteluluettelo, joka lataa ottelut JSON-aineistosta ja mahdollistaa
    /// niiden hakemisen.
    /// </summary>
    public class MatchCollection
    {
        const string JSON_DATABASE_FILENAME = @"C:\matches.json";

        private MatchCollection() { }

        List<Match> Matches;

        private static MatchCollection _instance;

        /// <summary>
        /// Palauttaa instanssin tästä luokasta (singleton).
        /// </summary>
        /// <returns>Tämän luokan instanssin.</returns>
        public static MatchCollection Instance()
        {
            if (_instance == null)
            {
                _instance = new MatchCollection();
                _instance.LoadMatches();
            }

            return _instance;
        }

        /// <summary>
        /// Lataa ottelut JSON-aineistosta.
        /// </summary>
        private void LoadMatches()
        {
            string json = Encoding.UTF8.GetString(Properties.Resources.matches);

            Matches = JsonConvert.DeserializeObject<List<Match>>(json);
        }

        /// <summary>
        /// Hakee ja palauttaa ottelut tietyltä aikaväliltä.
        /// </summary>
        /// <param name="startTime">Aikavälin alku.</param>
        /// <param name="endTime">Aikavälin loppu.</param>
        /// <returns>Listan otteluista määritellyltä aikaväliltä.</returns>
        public List<Match> GetMatches(DateTime startTime, DateTime endTime)
        {
            return Matches.FindAll(m => m.MatchDate >= startTime && m.MatchDate <= endTime);
        }

        /// <summary>
        /// Palauttaa aineiston vanhimman ottelun aloitusajan.
        /// </summary>
        /// <returns>DateTime-rakenteen joka vastaa vanhimman ottelun aloitusaikaa.</returns>
        public DateTime GetTimeOfFirstMatch()
        {
            return Matches.OrderBy(m => m.MatchDate).ElementAt(0).MatchDate;
        }

        /// <summary>
        /// Palauttaa aineiston viimeisimmän ottelun aloitusajan.
        /// </summary>
        /// <returns>DateTime-rakenteen joka vastaa viimeisimmän ottelun aloitusaikaa.</returns>
        public DateTime GetTimeOfLastMatch()
        {
            return Matches.OrderBy(m => m.MatchDate).Reverse().ElementAt(0).MatchDate;
        }
    }
}