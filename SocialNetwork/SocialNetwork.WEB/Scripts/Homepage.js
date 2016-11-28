var homepage = function () {

    var result = {};

    var totalHashtagsCountInHashtagsContainer = 0;

    result.changeTheDisplayedPosts = function () {
        var myPublications = $('#myPublications');
        var myReposts = $('#myReposts');

        var writePostContainer = $('#write-post-container');
        var publicationingPost = $('#publicationingPost');
        var publicationsContainer = $('#publicationsContainer');
        var repostsContainer = $('#repostsContainer');

        if (myReposts.css("display") == "none") {
            myPublications.hide();
            writePostContainer.hide();
            publicationsContainer.hide();
            publicationingPost.hide();

            myReposts.show();
            repostsContainer.show();
        }
        else {
            myPublications.show();
            writePostContainer.show();
            publicationsContainer.show();

            repostsContainer.hide();
            myReposts.hide();
        }


    }
    result.closePostForm = function () {
        $('#write-post-container').show();
        $('#publicationingPost').hide();
        $('#newPostContent').val("");
        $('#incorrectHashtagsError').hide();
        $('#hashtagsContainer').empty();
    }
    result.createNewPost = function () {
        $('#write-post-container').hide();
        $('#publicationingPost').show();
        $('#newPostContent').val("");
        $('#hashtagsContainer').empty();
    }
    result.checkPostForm = function () {
        $("name=Hashtags").each(function (index, value) {
            value.each(function (index, nestedValue) {
                if (value == nestedValue) {
                    $('#incorrectHashtagsError').show();
                    return false;
                }

            });
        });
        var postContent = $('#newPostContent').val();
        if (postContent == "" || postContent == null || postContent.split(" ") == "") { return false; }
        $('#incorrectHashtagsError').hide();
        closePostForm();

        return true;
    }
    result.addHashtag = function () {
        var hashtagsContainer = $('#hashtagsContainer');
        var newId = 'hashtagSet' + totalHashtagsCountInHashtagsContainer;
        var newDeleteElementId = 'removeSpan' + totalHashtagsCountInHashtagsContainer;
        totalHashtagsCountInHashtagsContainer++;

        hashtagsContainer.append('<div id=' + newId + '><input type="text" name="Hashtags"><a  id=' + newDeleteElementId + '><span class="glyphicon glyphicon-remove"></span></a></div>');

        $('#' + newDeleteElementId).click(function () {
            var removingDivId = $('#' + newDeleteElementId).parent().attr('id');
            $('#' + removingDivId).remove();

        });

    }

    result.removeNoPublicationsErrorMessage = function () {
        $('#noPublicationsErrorMessage').remove();
    }

    $(document).ready(function () {
        var publishedPostsLikes = 0;
        var publishedPostsLikesId;

        var repostsLikes = 0;
        var repostsLikesId;

        var identityName = "authorizedProfile";//
        var publicationsContainer = $('#publicationsContainer');

        $.ajax({
            type: 'POST',
            url: '/Post/Publications',
            data: { "offset": 0, "identityName": identityName, "count": 10, "postType": "publications" },

            success: function (jsonResult) {
                var publicationsContainer = $('#publicationsContainer');
                var result = JSON.parse(jsonResult);

                if (result["errorMessage"]) {
                    $('#publicationsContainer').empty();
                    $('#publicationsContainer').append('<div id="noPublicationsErrorMessage" style="text-align:center;"class="common-info-block-text">' + result["errorMessage"] + '</div>');
                    return;
                }
                else {
                    //alert(jsonResult);
                    post = "";
                    for (var i = 0; i < result.length; i++) {
                        publishedPostsLikesId = publishedPostsLikes + "LikeContainer";
                        post += '<div class="post-container common-info-block-text">';
                        // post += '<div>' + result[i]["PublisherName"] + " " + result[i]["PublisherSername"] + " (" + result[i]["PublisherIdentityName"] + ')</div>';
                        post += '<div>' + result[i]["PublishDate"].replace("T", " at ") + '</div>';
                        post += '<div class="hashtags-container">';

                        for (var j = 0; j < result[i]["Hashtags"].length; j++) {
                            post += ' <div class="hashtag">' + result[i]["Hashtags"][j] + '</div>';
                        }
                        post += '</div><hr/>';
                        post += '<div>' + result[i]["Content"] + '</div><hr/>';//onclick="Like(' + result[i]["Id"] + ',' + publishedPostsLikesId + ')"
                        post += '<div><a onclick="common.setLike(' + result[i]["Id"] + ',' + "'" + publishedPostsLikesId + "'" + ')"><span class="glyphicon glyphicon-heart"></span></a> Likes <span id=' + publishedPostsLikesId + '>' + result[i]["Likes"] + '</span>';
                        post += '&nbsp&nbspReposts ' + result[i]["Reposts"] + '</div>';
                        post += '</div><br>';

                        publishedPostsLikes++;
                        publicationsContainer.append(post);
                        post = "";
                    }
                }


            }
        });

        $.ajax({
            type: 'post',
            url: '/Post/Publications',
            data: { "offset": 0, "identityName": identityName, "count": 10, "postType": "reposts" },
            success: function (jsonResult) {
                var repostsContainer = $('#repostsContainer');
                var result = JSON.parse(jsonResult);

                if (result["errorMessage"]) {
                    repostsContainer.empty();
                    repostsContainer.append('<div id="noRepostsErrorMessage" style="text-align:center;"class="common-info-block-text">' + result["errorMessage"] + '</div>');
                    return;
                }
                else {
                    post = "";
                    for (var i = 0; i < result.length; i++) {
                        repostsLikesId = repostsLikes + "repostLikeContainer";
                        post += '<div class="post-container common-info-block-text">';
                        post += '<div>' + result[i]["PublisherName"] + " " + result[i]["PublisherSername"] + " (" + result[i]["PublisherIdentityName"] + ')</div>';
                        post += '<div>' + result[i]["PublishDate"].replace("T", " at ") + '</div>';
                        post += '<div class="hashtags-container">';

                        for (var j = 0; j < result[i]["Hashtags"].length; j++) {
                            post += ' <div class="hashtag">' + result[i]["Hashtags"][j] + '</div>';
                        }
                        post += '</div><hr/>';
                        post += '<div>' + result[i]["Content"] + '</div><hr/>';//onclick="Like(' + result[i]["Id"] + ',' + publishedPostsLikesId + ')"
                        post += '<div><a onclick="setLike(' + result[i]["Id"] + ',' + "'" + repostsLikesId + "'" + ')"><span class="glyphicon glyphicon-heart"></span></a> Likes <span id=' + repostsLikesId + '>' + result[i]["Likes"] + '</span>';
                        post += '&nbsp&nbspReposts ' + result[i]["Reposts"] + '</div>';
                        post += '</div><br>';

                        repostsLikes++;
                        publicationsContainer.append(post);
                        post = "";
                    }
                }

            }

        });

    });

    return result;
}();