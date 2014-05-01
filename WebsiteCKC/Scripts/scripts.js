$(function () {
    var newTeamActive = false;
    var adjustButton = '<button class="btn btn-xs btn-primary adjust-row"><span class="glyphicon glyphicon-pencil"></span></button>';
    var clubInputField = '<input type="text" class="form-control" placeholder="Bijv: CKC" />';
    var teamInputField = '<input type="text" class="form-control" placeholder="Bijv: 5" />';
    var AdjustOkButton = '<button class="btn btn-sm btn-primary adjust-row-ok new">Change</button>';
    var deleteButton = '<button class="btn btn-xs btn-danger delete-row"><span class="glyphicon glyphicon-remove"></span></button>';
    var buttonDivider = '<span class="button-divider" style="color: #808080">|</span>';

    function AdjustTeam(trElement)
    {
        $(trElement).find("td").each(function (i, v) {

            if(i == 1)
            {
                var value = $(v).html();
                $(v).html(clubInputField);
                $(v).find("input").val(value);
            }
            if (i == 2) {
                var value = $(v).html();
                $(v).html(clubInputField);
                $(v).find("input").val(value);
            }

            if( i == 3 )
            {
                $(v).html(AdjustOkButton);
            }
        });
    }

    function CheckInput(clubName, teamNumber) {
        // Validate data
        if (clubName.length > 0 && teamNumber.length > 0)
        {
            if ($.isNumeric(teamNumber))
            {
                console.log(clubName + teamNumber)
                return true;
            }
            return false;
        }

        return false;
        
    }

    // Adjust existing team
    $(document).on('click', '.adjust-row', function () {
        AdjustTeam($(this).parents()[1]);
    });

    // Opening the add-team form
    $(document).on('click', '.add-team-row', function () {
        if (!newTeamActive) {
            newTeamActive = true;
            var row = '<tr><td class="col-xs-1">#</td><td class="col-xs-3"><input type="text" class="form-control" placeholder="Bijv: CKC" /></td><td class="col-xs-2"><input type="text" class="form-control" placeholder="Bijv: 5" /></td><td class="col-xs-6"><button class="btn btn-sm btn-primary submit-row new">Add</button></td></tr>';
            $(".table-body").append(row);

            // Werkt
            $(".table-body").find(".new").each(function (index, value) {
                if (index == 1) {
                    $(value).find("input").focus();
                }
            })
        }
    });

    // Submit adjustment to a existing team
    $(document).on('click', '.adjust-row-ok', function () {
        // Get the whole row
        var row = $(this).parents()[1];
        var teamID = $(row).data("id");
        var compID = $("#competitionID").val();
        var id = $($(this).parents()[1]).data('id');

        // Get the values and put them in the array
        var arr = [];

        $(row).find("td").each(function (index, value) {
            var value = $(value).find("input").val();
            if(value != undefined )
            {
                arr.push(value);
            }
        });

        if(CheckInput(arr[0], arr[1]))
        {
            // Push to server. -> Ajax call
            $.ajax({
                type: "POST",
                url: "/Admin/AdjustTeam",
                data: { teamID: parseInt(id), clubName: arr[0], teamNumber: parseInt(arr[1]) },
                success: function () {
                    $(row).find("td").each(function (index, value) {
                        if (index == 1 || index == 2) {
                            var v = $(value).find("input").val();
                            $(value).html(v);
                        }
                        if (index == 3) {
                            $(value).replaceWith("<td class='col-xs-6'>" + adjustButton + buttonDivider + deleteButton + "</td>");
                        }
                    });
                },
                error: function () {

                }
            });
            
        }
        else
        {
            ShowModal("Fout bij het opslaan van uw aanpassing.")
        }
    });

    // Adding a new team
    $(document).on('click', '.submit-row', function () {
        // Get the whole row
        var row = $(this).parents()[1];
        var competitionID = $("#competitionID").val();

        var arr = [];
        $(row).find("td").each(function (index, value) {
            var value = $(value).find("input").val();
            if (value != undefined) {
                arr.push(value);
            }
        });

        if (CheckInput(arr[0], arr[1]))
        {
            $.ajax({
                type: "POST",
                url: "/Admin/AddTeam",
                data: { compID: competitionID, clubName: arr[0], teamNumber: parseInt(arr[1]) },
                success: function (data) {
                    console.log(data);
                    $(row).data("id", data);
                    $(row).find("td").each(function (index, value) {
                        var inputValue = $(value).find("input").val();
                        $(value).find("input").remove();
                        $(value).html(inputValue);

                        if (index == 3) {
                            $(value).replaceWith("<td class='col-xs-6'>" + adjustButton + buttonDivider + deleteButton + "</td>");

                            newTeamActive = false;
                        }
                    });
                },
                error: function () {

                }
            })
        }
        else
        {
            $('#myModal').modal('show');
        }
    });

    // Deleting a row
    $(document).on('click', '.delete-row', function () {
        var id = $($(this).parents()[1]).data('id');
        var trRow = $(this).parents()[1];
        console.log(trRow);
        // Ajax Call

        $.ajax({
            type: "POST",
            url: "/Admin/DeleteTeam",
            data: { teamID: id },
            success: function () {
                $(trRow).remove();
            },
            error: function () {

            }
        });

    });

    // Setting a favorite team
    $(document).on('click', '.set-favorite', function () {
        var row = $(this).parents()[1];
        var teamID = $(row).data("id");
        var compID = $("#competitionID").val();

        // Remove old fav team
        $(".table-body").find(".favorite-team").remove();

        // Set the fav team

        $.ajax({
            url: '/Admin/SetFavoriteTeam/',
            type: "POST",
            data: { compID: compID, teamID: teamID },
            success: function (data) {
                // We have setted a favorite team. Lets remove the rest of the 
            },
            error: function () {

            }
        })
    })

    function SubmitAdjust()
    {

    }

    function SubmitTeam() {

    }

    function ShowModal(displayText)
    {
        $("#myModalLabel").text(displayText);
        $('#myModal').modal('show');
    }
});