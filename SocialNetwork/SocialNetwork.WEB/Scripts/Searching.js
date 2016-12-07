var searching = function () {
    var totalMarksCount = 0;
    var result = {};

     var profileBlockInfo = 0;
     var profileBlockInfoId;
     var profilePublicationsContainerId;
     var signinResultId;

    result.subscribe = function (identityName, signinResultContainerId) {
        $.ajax({
            type: 'post',
            url: '/ProfileInteraction/Subscribe',
            data: { "targetIdentity": identityName },
            success: function (jsonResult) {
                var result = JSON.parse(jsonResult);
                //alert(jsonResult);
                /*$('#' + signinResultContainerId).html(result["completionMessage"]);
                if ($('#' + signinResultContainerId).css("display") == "none") {
                    $('#' + signinResultContainerId).show();
                }*/
            }
        });
    }

    result.getProfileStatistics = function (identityName) {
        common.statistics(identityName, 'modalStatisticsBody');
    }
    result.getPublications = function (identityName, publicationsPlaceId) {
        if ($('#' + publicationsPlaceId).html() != "") {
            $('#' + publicationsPlaceId).slideToggle("500");
            return;
        }

        $.ajax({
            type: 'POST',
            url: '/Post/Publications',
            data: { "identityName": identityName, "postType": "publications", "offset": 0 },
            success: function (jsonResult) {
                var result = JSON.parse(jsonResult);
                var publicationsPlace = $('#' + publicationsPlaceId);
                publicationsPlace.html("");
                for (var i = 0; i < result.length; i++) {
                    publicationsPlace.append(common.createPostContainer(result[i]));
                }
            }
        });
    }

    result.getCanvasSize = function() {
        alert($('#mostPopularHashtags').size());
    }

     result.createProfileContainer = function(profileData) {
         
         profileBlockInfoId = 'profileBlockInfo' + profileBlockInfo;
         profilePublicationsContainerId = 'profilePublicationsContainer' + profileBlockInfo;
         signinResultId = 'signinResult' + profileBlockInfoId;

        var container = "";
        container += '<div class="row">';
        container += '<div class="col-md-12">';

        container += '<div class="common-info-block common-info-block-text" style="width:100%;" data-toggle="collapse" data-target="' + '#' + profileBlockInfoId + '")">';
        container += '<h4 class="h4LowMarginBottom">' + profileData["Name"] + " " + profileData["Sername"] + " (" + profileData["IdentityName"] + ')</h4>';
        container += '<h3 style="display:none;color:green;" id="' + signinResultId + '"></h3>';
        container += '</div>';

        container += '<div class="common-info-block common-info-block-text" style="width:100%;">';
        container += '<div class="adaptiveContainer  collapse"  id="' + profileBlockInfoId + '" style="width:100%;">';

        container += '<hr class="lowHrMargine"/>';
        //container += '<h4>Date of birth : ' + profileData["DateOfBirth"] + '</h4>';
        container += '<h4>Followers : ' + profileData["Followers"].length + '</h4>';
        container += '<h4>Subscriptions : ' + profileData["Subscriptions"].length + '</h4>';
        container += '<hr class="lowHrMargine"/>';
        container += '<h4>Status</h4>';
        container += '<h4>' + profileData["CustomInfo"] + '</h4>';

        container += '<hr class="lowHrMargine"/>';

        container += '<div class="row">';


        //onclick="searching.getProfileStatistics(' + "'" + profileData["IdentityName"] + "'" + ')"
        container += '<div class="col-md-4">';
        container += '<button class="btn btn-default searchButton common-info-block-text" data-toggle="modal" data-target="#statisticsModal">';
        container += '<h5>Statistics</h5>';
        container += '</button>';
        container += '</div>';

        container += '<div class="col-md-4">';
        container += '<button class="btn btn-default searchButton common-info-block-text" onclick="searching.getPublications(' + "'" + profileData["IdentityName"] + "'" + ',' + "'" + profilePublicationsContainerId + "'" + ')">';
        container += '<h5>Publications</h5>';
        container += '</button>';
        container += '</div>';

        container += '<div class="col-md-4">';
        container += '<button class="btn btn-success searchButton common-info-block-text" style="color:white;" onclick="searching.subscribe(' + "'" + profileData["IdentityName"] + "'" + ',' + "'" + signinResultId + "'" + ')">';
        container += '<h5>Subscribe</h5>';
        container += '</button>';
        container += '</div></div></div></div><br>';

        container += '<div class="adaptiveContainer" id="' + profilePublicationsContainerId + '">';
        container += '</div>';
        container += '</div>';
        container += '</div>';
        container += '<br>';

        profileBlockInfo++;
        return container;
    }

    result.addHashtag = function () {
        var marksContainer = $('#marksContainer');
        var newId = 'markContainer' + totalMarksCount;
        var removeElement = 'removeSpan' + totalMarksCount;
        var removingBr = 'br' + totalMarksCount;

        var appendString = "";
        appendString += '<input type="text" class="searchingHashtag" name="mark" id=' + newId + '>';
        appendString += '<a id=' + removeElement + '><span class="glyphicon glyphicon-remove"></span></a>';


        marksContainer.append(appendString);

        $('#' + removeElement).click(function () {
            var removingRowId = $('#' + newId);
            removingRowId.remove();
            $('#' + removeElement).remove();
        });
        totalMarksCount++;
    };


    result.findContent = function () {
        var marksCollection = [];
        $('input[name="mark"]').each(function () {
            marksCollection.push($(this).val());
        });
        var searchingType = "";
        var selectedElement = $('#filter option:selected').val();

        switch (selectedElement) {
            case "PostsByMarks": searchingType = "PostsByMarks"; break;
            case "PostsByMyStatistics": searchingType = "PostsByDefault"; break;
            case "ProfilesByMarks": searchingType = "ProfilesByMarks"; break;
            case "ProfilesByMyStatistics": searchingType = "ProfilesByDefault"; break;

        }

        var dataPlace = $('#dataPlace');
        var url = "";
        var lengthZeroError = "";
        var handler = null;

        if (selectedElement == "PostsByMarks" || selectedElement == "PostsByMyStatistics") {
            url = "/Search/FindPosts";
            lengthZeroError = '<h4>There are no posts found</h4>';
            handler = common.createPostContainer;
        }

        if (selectedElement == "ProfilesByMarks" || selectedElement == "ProfilesByMyStatistics") {
            url = "/Search/FindProfiles";
            lengthZeroError = '<h4>There are no profiles found</h4>';
            handler = searching.createProfileContainer;
        }


        $.ajax({
            type: 'POST',
            url: url,
            data: { "hashtags": marksCollection, "searchingType": searchingType },
            success: function (jsonResult) {
                result = JSON.parse(jsonResult);
                if (result["errorMessage"]) {
                    dataPlace.html('<h4>' + result["errorMessage"] + '</h4>');
                    return;
                }
                if (result.length == 0) {
                    dataPlace.html(lengthZeroError);
                    return;
                }

                dataPlace.html("");
                //dataPlace.append('<h4>'+selectedElement+'</h4>');
                for (var i = 0; i < result.length; i++) {
                    dataPlace.append(handler(result[i]));
                }
            }
        });
    };

    return result;
}();