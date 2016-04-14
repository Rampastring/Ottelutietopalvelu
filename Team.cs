// @author Rami Pasanen
// @version 12.04.2016

using System.Drawing;

namespace WebApplication
{
    /// <summary>
    /// Joukkue.
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Joukkueen yksilöllinen ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Joukkueen (lyhennetty) nimi.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Joukkueen (täysi) nimi.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Joukkueen logo.
        /// </summary>
        public Image Logo { get; set; }

        /// <summary>
        /// URL-osoite, josta joukkueen logo löytyy.
        /// </summary>
        public string LogoURL { get; set; }

        /// <summary>
        /// Joukkueen sijoitus.
        /// </summary>
        public int Ranking { get; set; }

        /// <summary>
        /// Joukkueen viesti.
        /// </summary>
        public string Message { get; set; }
    }
}