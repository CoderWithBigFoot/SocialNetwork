var common = function () {
    var result = {};

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


    return result;
}();