var common = function () {
    var result = {};

    //For createPostContainer(id's for likes)
    var allPostsLikes = 0;
    var allPostsLikesId;


    result.setLike = function(postId, publishedPostLikeId) {
        var identityName = 'authorizedProfile';
        $.ajax({
            type: 'POST',
            url: '/Post/Like',
            data: { "postId": postId, "identityName": identityName },
            success: function (jsonResult) {
                var result = JSON.parse(jsonResult);
                $('#' + publishedPostLikeId).html(result["newLikesCount"]);

            }
        });
    }
    result.createPostContainer = function (postData) {
        var post = "";
        
        if (postData["errorMessage"] != null) { return;}

            allPostsLikesId = allPostsLikes + "LikeContainer";

            post += '<div class="post-container common-info-block-text">';
            post += '<div>' + postData["PublisherName"] + " " + postData["PublisherSername"] + " (" + postData["PublisherIdentityName"] + ')</div>';
            post += '<div>' + postData["PublishDate"].replace("T", " at ") + '</div>';
            post += '<div class="hashtags-container">';

            for (var j = 0; j < postData["Hashtags"].length; j++) {
                post += ' <div class="hashtag">' + postData["Hashtags"][j] + '</div>';
            }
            post += '</div><hr/>';
            post += '<div>' + postData["Content"] + '</div><hr/>';
            post += '<div><a onclick="common.setLike(' + postData["Id"] + ',' + "'" + allPostsLikesId + "'" + ')"><span class="glyphicon glyphicon-heart"></span></a> Likes <span id=' + allPostsLikesId + '>' + postData["Likes"] + '</span>';
            post += '&nbsp&nbspReposts ' + postData["Reposts"] + '</div>';
            post += '</div><br>';

            allPostsLikes++;
            return post;
        
    }

    result.homepage = function () {
        $.ajax({
            type: "GET",
            url: "/Common/HomepagePartial",
            success: function (partialViewResult) {
                $('#partialsPlace').html(partialViewResult);
            }
        });
    };
    result.statistics = function (identityName) {
        $.ajax({
            type: "POST",
            url: "/Statistics/StatisticsPartial", //here is error(incorrect partials path,becouse this function called from CommonController)
            data:{"identityName":identityName},
            success: function (partialViewResult) {
                $('#partialsPlace').html(partialViewResult);
            }
        });
    };
    result.searching = function () {
        $.ajax({
            type: "POST",
            url: "/Search/SearchPartial",
            success: function (partialViewResult) {
                $('#partialsPlace').html(partialViewResult);
            }
        });
    };
   


    return result;
}();