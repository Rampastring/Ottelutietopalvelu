// @author Rami Pasanen
// @version 12.04.2016

using System;

namespace WebApplication
{
    /// <summary>
    /// Ottelu.
    /// </summary>
    public class Match
    {
        /// <summary>
        /// Ottelun aloituspäivämäärä ja -aika.
        /// </summary>
        public DateTime MatchDate { get; set; }

        /// <summary>
        /// Vierasjoukkueen tekemien maalien määrä.
        /// </summary>
        public int AwayGoals { get; set; }

        /// <summary>
        /// Vierasjoukkue.
        /// </summary>
        public Team AwayTeam { get; set; }

        /// <summary>
        /// Kotijoukkueen tekemien maalien määrä.
        /// </summary>
        public int HomeGoals { get; set; }

        /// <summary>
        /// Kotijoukkue.
        /// </summary>
        public Team HomeTeam { get; set; }
    }
}