<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ottelutietopalvelu.aspx.cs" Inherits="WebApplication.Ottelutietopalvelu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ottelutietopalvelu</title>
    <link href="Ottelutietopalvelu.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-header">Ottelutietopalvelu</div>
        <table class="search-table">
            <thead>
                <tr>
                    <td colspan="2" class="center">Hae ottelut aikaväliltä</td>
                </tr>
            </thead>
            <tbody class="search-table-tbody">
                <tr>
                    <td class="center">Aloituspäivä:</td>
                    <td class="center">Lopetuspäivä:</td>
                </tr>
                <tr>
                    <td class="center">
                        <asp:DropDownList ID="ddStartDay" runat="server" Width="50" CssClass="ddl" onchange="checkButtonEnable()">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddStartMonth" runat="server" Width="50" CssClass="ddl" onchange="setStartTimeDays()">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddStartYear" runat="server" Width="80" CssClass="ddl" onchange="setStartTimeDays()">
                        </asp:DropDownList>
                    </td>
                    <td class="center">
                        <asp:DropDownList ID="ddEndDay" runat="server" Width="50" CssClass="ddl" onchange="checkButtonEnable()">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddEndMonth" runat="server" Width="50" CssClass="ddl" onchange="setEndTimeDays()">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddEndYear" runat="server" Width="80" CssClass="ddl" onchange="setEndTimeDays()">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="center">
                        <asp:Button ID="btnFind" runat="server" Text="Hae »" Width="120" Height="30" CssClass="button" />
                    </td>
                </tr>
            </tbody>
            </table>
        <p id="message">
            &nbsp;</p>
        <span runat="server" id="matchList"></span>
    </form>
    <script>
        
        /**
         * Palauttaa päivien määrän tietyssä kuukaudessa.
         * @param year Vuosi.
         * @param month Kuukausi.
         */
        function daysInMonth(year, month) {
            var date = new Date(year, month, 0).getDate();
            return date;
        }

        /**
         * Asettaa aloitusajan päivien määrän käyttöliittymässä.
         */
        function setStartTimeDays() {
            var ddDay = document.getElementById("<%=ddStartDay.ClientID%>");
            var ddMonth = document.getElementById("<%=ddStartMonth.ClientID%>");
            var ddYear = document.getElementById("<%=ddStartYear.ClientID%>");

            setDays(ddDay, ddMonth, ddYear);

            checkButtonEnable();
        }

        /**
         * Asettaa aikavälin lopun päivien määrän käyttöliittymässä.
         */
        function setEndTimeDays() {
            var ddDay = document.getElementById("<%=ddEndDay.ClientID%>");
            var ddMonth = document.getElementById("<%=ddEndMonth.ClientID%>");
            var ddYear = document.getElementById("<%=ddEndYear.ClientID%>");

            setDays(ddDay, ddMonth, ddYear);

            checkButtonEnable();
        }

        /**
         * Tarkistaa, onko käyttäjän valitsema otteluhaun aikaväli hyväksyttävä
         * (eli aloitusaika on ennen lopetusaikaa) ja joko ottaa Hae-näppäimen
         * pois käytöstä tai ottaa sen käyttöön.
         */
        function checkButtonEnable() {
            var ddStartDay = document.getElementById("<%=ddStartDay.ClientID%>");
            var ddStartMonth = document.getElementById("<%=ddStartMonth.ClientID%>");
            var ddStartYear = document.getElementById("<%=ddStartYear.ClientID%>");

            var ddEndDay = document.getElementById("<%=ddEndDay.ClientID%>");
            var ddEndMonth = document.getElementById("<%=ddEndMonth.ClientID%>");
            var ddEndYear = document.getElementById("<%=ddEndYear.ClientID%>");

            if (ddEndYear.selectedIndex > ddStartYear.selectedIndex) {
                enableFindButton();
                return;
            }

            if (ddEndYear.selectedIndex < ddStartYear.selectedIndex) {
                disableFindButton();
                return;
            }

            // Tänne päästään vain, jos pudotusvalikoista on valittuina samat vuodet

            if (ddEndMonth.selectedIndex > ddStartMonth.selectedIndex) {
                enableFindButton();
                return;
            }

            if (ddEndMonth.selectedIndex < ddStartMonth.selectedIndex) {
                disableFindButton();
                return;
            }

            // Tänne päästään vain, jos pudotusvalikoista on valittuina samat kuukaudet

            if (ddEndDay.selectedIndex < ddStartDay.selectedIndex) {
                disableFindButton();
                return;
            }

            enableFindButton();
        }

        /**
         * Ottaa hakunäppäimen pois käytöstä.
         */
        function disableFindButton() {
            var btnFind = document.getElementById("<%=btnFind.ClientID%>");
            btnFind.disabled = true;
            btnFind.className = "button-disabled";
        }

        /**
         * Ottaa hakunäppäimen käyttöön.
         */
        function enableFindButton() {
            var btnFind = document.getElementById("<%=btnFind.ClientID%>");
            btnFind.disabled = false;
            btnFind.className = "button";
        }

        /**
         * Apufunktio, joka lisää päiviä pudotusvalikkoon ja 
         * valitsee päivävalitsimesta käyttäjän toivoman indeksin,
         * kun kuukautta vaihdetaan.
         * @param ddDay Päivien pudotusvalikko.
         * @param ddMonth Kuukausien pudotusvalikko.
         * @param ddYear Vuosien pudotusvalikko.
         */
        function setDays(ddDay, ddMonth, ddYear) {
            var selectedYear = ddYear.options[ddYear.selectedIndex].text;

            var selectedDayIndex = ddDay.selectedIndex;
            var isLastIndex = (ddDay.options.length - 1) == selectedDayIndex;

            var dayCount = daysInMonth(parseInt(selectedYear), ddMonth.selectedIndex + 1);

            addDaysToDropDown(ddDay, dayCount);

            if (ddDay.options.length <= selectedDayIndex || isLastIndex) {
                ddDay.selectedIndex = ddDay.options.length - 1;
            } else {
                ddDay.selectedIndex = selectedDayIndex;
            }

            return false;
        }

        /**
         * Poistaa kaikki vaihtoehdot pudotusvalikosta ja lisää siihen tietyn määrän päiviä.
         * @param dropdown Pudotusvalikko.
         * @param dayCount Lisättävien päivien määrä.
         */
        function addDaysToDropDown(dropdown, dayCount) {
            var length = dropdown.options.length;
            for (i = length - 1; i >= 0; i--) {
                dropdown.remove(i);
            }

            for (i = 1; i <= dayCount; i++) {
                var option = document.createElement("option");
                option.text = i.toString();
                dropdown.options.add(option);
            }
        }
    </script>
</body>
</html>
