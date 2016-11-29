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
        var allPostsLikes = 0;
        var allPostsLikesId;

        var identityName = "authorizedProfile";
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
                    for (var i = 0; i < result.length; i++) {
                        publicationsContainer.append(common.createPostContainer(result[i]));
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
                    for (var i = 0; i < result.length; i++) {
                        publicationsContainer.append(common.createPostContainer(result[i]));
                    }
                }
            }

        });

        });

   

    return result;
}();