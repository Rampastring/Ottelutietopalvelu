using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    /// <summary>
    /// Luokka sivulle.
    /// </summary>
    public partial class Ottelutietopalvelu : Page
    {
        /// <summary>
        /// Päivittää sivulla näytetyt ottelut.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            int startDay = Int32.Parse(ddStartDay.SelectedValue);
            int startMonth = Int32.Parse(ddStartMonth.SelectedValue);
            int startYear = Int32.Parse(ddStartYear.SelectedValue);
            int endDay = Int32.Parse(ddEndDay.SelectedValue);
            int endMonth = Int32.Parse(ddEndMonth.SelectedValue);
            int endYear = Int32.Parse(ddEndYear.SelectedValue);

            DateTime startTime = new DateTime(startYear, startMonth, startDay);
            DateTime endTime = new DateTime(endYear, endMonth, endDay, 23, 59, 59);

            List<Match> matches = MatchCollection.Instance.GetMatches(startTime, endTime);

            matchList.InnerHtml = String.Empty;

            if (matches.Count > 0)
            {
                StringBuilder html = new StringBuilder();

                bool isDark = true;

                html.Append("<table class=\"match-list-header\"><tbody><tr><td colspan=\"2\">Ottelut</td></tr>");
                html.Append("<tr><td class=\"match-list-subheader\">Kotijoukkue</td>");
                html.Append("<td class=\"match-list-subheader\">Vierasjoukkue</tr></td></tbody></table>");

                foreach (Match match in matches)
                {
                    if (isDark)
                        html.Append("<table class=\"match-table-dark\">");
                    else
                        html.Append("<table class=\"match-table\">");

                    html.Append("<tbody><tr class=\"match-tr\"><td colspan=\"2\" class=\"match-date\">");
                    html.Append(match.MatchDate.ToShortDateString());
                    html.Append("</td></tr>");

                    html.Append(string.Format(
                        "<tr class=\"match-tr\"><td class=\"match-team-info\"><img src=\"{0}\" class=\"match-team-img-left\" />{1} {2}</td>" +
                        "<td class=\"match-team-info\">{4} {5}<img src=\"{3}\" class=\"match-team-img-right\" /></td></tr>",
                        match.HomeTeam.LogoURL, match.HomeTeam.FullName, match.HomeGoals,
                        match.AwayTeam.LogoURL, match.AwayGoals, match.AwayTeam.FullName));

                    html.Append("</tbody></table>");

                    isDark = !isDark;
                }

                matchList.InnerHtml = html.ToString();
            }
            else
            {
                matchList.InnerHtml = "<p>Annetulla aikavälillä ei ole pelattu yhtään ottelua.</p>";
            }
        }

        /// <summary>
        /// Alustaa sivun. Määrittää aikavälivalitsimien vaihtoehdot
        /// otteluaineiston perusteella.
        /// </summary>
        protected void Page_Init(object sender, EventArgs e)
        {
            DateTime firstMatchTime = MatchCollection.Instance.GetTimeOfFirstMatch();
            DateTime lastMatchTime = MatchCollection.Instance.GetTimeOfLastMatch();

            for (int i = firstMatchTime.Year; i <= lastMatchTime.Year; i++)
            {
                ListItem li = new ListItem(i.ToString(), i.ToString());

                ddStartYear.Items.Add(li);
                ddEndYear.Items.Add(li);
            }

            for (int i = 1; i < 13; i++)
            {
                ddStartMonth.Items.Add(i.ToString());
                ddEndMonth.Items.Add(i.ToString());
            }

            for (int i = 1; i < 32; i++)
            {
                ddStartDay.Items.Add(i.ToString());
                ddEndDay.Items.Add(i.ToString());
            }

            // Valitaan automaattisesti viimeinen päivä
            ddEndYear.SelectedIndex = ddEndYear.Items.Count - 1;
            ddEndMonth.SelectedIndex = ddEndMonth.Items.Count - 1;
            ddEndDay.SelectedIndex = ddEndDay.Items.Count - 1;
        }
    }
}