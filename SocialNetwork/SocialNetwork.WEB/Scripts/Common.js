﻿var common = function () {
    var result = {};

    //For createPostContainer(id's for likes)
    var allPostsLikes = 0;
    var allPostsLikesId;

    var allReposts = 0;
    var allRepostsId;

    result.setLike = function(postId, publishedPostLikeId) {
        $.ajax({
            type: 'POST',
            url: '/Post/Like',
            data: { "postId": postId},
            success: function (jsonResult) {
                var result = JSON.parse(jsonResult);
                $('#' + publishedPostLikeId).html(result["newLikesCount"]);

            }
        });
    }

   
    result.repost = function (postId,repostContainerId) {
       
        $.ajax({
            type: 'POST',
            url: '/Post/Repost',
            data: { "postId": postId },
            success: function (jsonResult) {
                var result = JSON.parse(jsonResult);
                $('#' + repostContainerId).html(result["newRepostsCount"]);
            }
        });
    }

    result.createPostContainer = function (postData) {
        var post = "";
        
        if (postData["errorMessage"] != null) { return;}

            allPostsLikesId = allPostsLikes + "LikeContainer";
            allRepostsId = allReposts + "RepostContainer";    

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
            post += '&nbsp&nbsp';
            post += '<a onclick="common.repost(' + postData["Id"] + ',' + "'" + allRepostsId + "'" + ')"><span class="glyphicon glyphicon-bullhorn"></span></a> Reposts <span id='+allRepostsId+'>' + postData["Reposts"] +'</span>'+'</div>';
            post += '</div><br>';

            allPostsLikes++;
            allReposts++;
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
    result.statistics = function (identityName,partialsPlace) {
        $.ajax({
            type: "POST",
            url: "/Statistics/StatisticsPartial", 
            data:{"identityName":identityName},
            success: function (partialViewResult) {
                $('#' + partialsPlace).html(partialViewResult);
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
    result.news = function () {
        $.ajax({
            type: 'get',
            url: "/ProfileInteraction/NewsPartial",
            success: function (partialViewResult) {
                $('#partialsPlace').html(partialViewResult);
            }
        });
    }

    result.testPartial = function (identityName, partialsPlace) {
        $.ajax({
            type: "POST",
            url: "/Statistics/SomeTestPartial",
            data: { "name": "some name here" },
            success: function (partialViewResult) {
                $('#' + partialsPlace).html(partialViewResult);
               // alert("data recieved");
            }
        });
    };


    return result;
}();