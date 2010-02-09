var pickFive = {
    allProjectHTML: "",
    fundedValue: -1,
    hoverColor: [247, 248, 151],
    highlightedColor: [196, 249, 192],

    createAllProjectList: function() {
        var unfundedHtml = projectSelection.unfundedListHTML;  //document.getElementById("unfundedProjectList").innerHTML;
        var fundedHtml = projectSelection.fundedListHTML;      //document.getElementById("fundedProjectList").innerHTML;

        unfundedHtml = this.addOnClickHandlerTo(unfundedHtml);
        fundedHtml = this.addOnClickHandlerTo(fundedHtml);
        var header = "<table><tr><td align=center><B><U>UNFUNDED PROJECTS</U></B></td><td align=center><B><U>FUNDED PROJECTS</U></B></td><td align=center><B><U>SUMMARY</U></B></td></tr>";
        var summary = '<td valign=top align=center>' +
				'# of Projects Selected: <span id="numberSelected">0</span><p/>' +
				'Total Value of Selection: <span id="totalValueOfSelection">0</span><p/>' +
				'Value of Funded Projects: <span id="valueOfFundedProjects">0</span><p/>' +
				'Value of Perfect Information: <span id="valueOfInformation">0</span><p/>' +
				'</td>';
        var body = "<tr><td>" + unfundedHtml + "</td><td>" + fundedHtml + "</td>" + summary + "</tr>";
        var footer = "</table>";
        this.allProjectHTML = header + body + footer;

        this.setFundedValue(portfolio.fundedPoints);
        //this.fundedValue = portfolio.fundedPoints;

    },

    addOnClickHandlerTo: function(htmlTags) {
        var newTag = '<tr onclick="pickFive.rowClicked(this);" onMouseover="pickFive.rowHover(this);" onMouseout="pickFive.rowLeave(this);">';
        htmlTags = htmlTags.replace(/<TR>/g, newTag);
        htmlTags = htmlTags.replace(/<tr>/g, newTag);
        return htmlTags;
    },

    rowClicked: function(row) {
        if (this.isValid(row)) {
            var fourthCell = row.cells[3];
            var totalValue = this.getTotalValueOfSelection();
            var numSelected = this.getNumSelected();
            var fundedValue = this.getFundedValue();
            var cellValue;
            if (document.all) {
                cellValue = fourthCell.innerText;
            } else {
                cellValue = fourthCell.textContent;
            }
            if (row.style.backgroundColor == this.toString(this.hoverColor) || row.style.backgroundColor == "" || row.style.backgroundColor == this.toRGB(this.hoverColor)) {
                if (numSelected == 5) {
                    alert("You have already selected five projects. Unselect something before you pick this one.");
                } else {
                    row.style.backgroundColor = this.toRGB(this.highlightedColor);
                    totalValue += parseInt(cellValue);
                    numSelected++;
                }
            } else {
                row.style.backgroundColor = "";
                totalValue -= parseInt(cellValue);
                numSelected--;
            }
            var valueOfInformation = Math.max(totalValue - fundedValue, 0);
            this.setTotalValueOfSelection(totalValue);
            this.setNumSelected(numSelected);
            this.setValueOfInformation(valueOfInformation);
        }
    },

    setValueOfInformation: function(valueOfInformation) {
        document.getElementById("valueOfInformation").innerHTML = valueOfInformation;
    },

    getFundedValue: function() {
        return this.fundedValue;
    },

    setFundedValue: function(rhs) {
        this.fundedValue = rhs;
    },

    getNumSelected: function() {
        return parseInt(document.getElementById("numberSelected").innerHTML);
    },

    setNumSelected: function(numSelected) {
        document.getElementById("numberSelected").innerHTML = numSelected;
    },

    getTotalValueOfSelection: function() {
        return parseInt(document.getElementById("totalValueOfSelection").innerHTML);
    },

    setTotalValueOfSelection: function(totalValue) {
        document.getElementById("totalValueOfSelection").innerHTML = totalValue;
    },
    log: function(location, message) {
        document.getElementById(location).innerHTML = message;
    },
    isValid: function(row) {
        var valid = false;
        if (row.cells.length == 4) {
            var result;
            if (document.all) {
                result = row.cells[2].innerText;
            } else {
                result = row.cells[2].textContent;
            }
            if (result == "Success" || result == "Failure") {
                valid = true;
            }
        }
        return valid;
    },

    rowHover: function(row) {
        if (this.isValid(row) && row.style.backgroundColor == "") {
            row.style.backgroundColor = this.toRGB(this.hoverColor);
        }
    },

    rowLeave: function(row) {
        if (this.isValid(row)) {
            //this.log("numberSelected","rowLeave(): row.style.backgroundColor = "+row.style.backgroundColor+", hoverColor = "+this.toRGB(this.hoverColor));
            if (row.style.backgroundColor == this.toString(this.hoverColor) || row.style.backgroundColor == this.toRGB(this.hoverColor)) {
                row.style.backgroundColor = "";
            }
        }
    },

    toString: function(color) {
        return "rgb(" + color[0] + ", " + color[1] + ", " + color[2] + ")";
    },
    toRGB: function(color) {
        var r = color[0].toString(16);
        var g = color[1].toString(16);
        var b = color[2].toString(16);
        if (r.length == 1) r = '0' + r;
        if (g.length == 1) g = '0' + g;
        if (b.length == 1) b = '0' + b;
        return "#" + r + g + b;
    }
}