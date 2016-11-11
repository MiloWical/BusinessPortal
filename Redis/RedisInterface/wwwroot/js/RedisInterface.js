window.onload = Initialize;

function Initialize()
{
    hideById("valuePane");

    var cacheKeyElements = 
    document.getElementsByClassName("cacheKey");

    var keyValueElement = document.getElementById('keyValue');

    for(var i = 0; i < cacheKeyElements.length; i++)
        cacheKeyElements[i].addEventListener("click", function (event) {

                var key = event.currentTarget.textContent;

                DoGetRequest("http://localhost:58003/RedisApi/GetCacheKeyValue", [["key", key]], function (xhttp) {
                    keyValueElement.textContent = xhttp.responseText;

                document.getElementById("keyValueHeader").textContent = key;

                hljs.highlightBlock(keyValueElement);

                showById('valuePane');
            })
        });

    document.getElementById("closeButton").addEventListener("click", function () {
        hideById("valuePane");
    });
}