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
        alert(postId);
    }
    result.createPostContainer = function (postData) {
        var post = "";
        
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


    return result;
}();