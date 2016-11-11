function CreateRequestObject(callbackFunc)
{
    var xhttp = new XMLHttpRequest();  
    
    xhttp.onreadystatechange = function() {
    
        if (this.readyState == 4 && this.status == 200) 
        {
                callbackFunc(xhttp);
        }
        else if(this.status != 200)
            throw "Error completing AJAX request (Response Code: "+this.status+")";
    };

    return xhttp;
}

function DoRequest(url, method, params, callbackFunc)
{
    var request = CreateRequestObject(callbackFunc);

    method = method.toUpperCase();

    if(method === "GET")
    {
        if((typeof params !== "undefined") && params != null)
            url = url + "?" + FormatParameters(params);

        request.open(method, url, true);
        request.send();
    }
    else if(method === "POST")
    {
        request.open(method, url, true);
        request.setRequestHeader("Content-type", "application/x-www-form-urlencoded");

        if((typeof params !== "undefined") && params != null)
            request.send(FormatParameters(params));
        else
            request.send();
    }
    else return;
}

function DoGetRequest(url, params, callbackFunc)
{
    DoRequest(url, "GET", params, callbackFunc);
}

function DoPostRequest(url, params, callbackFunc)
{
    DoRequest(url, "POST", params, callbackFunc);
}

function FormatParameters(params)
{
    if(typeof params === "undefined" || params == null)
        return null;

    formattedParams = [];

    for(i = 0; i < params.length; i++)
        formattedParams.push(encodeURIComponent(params[i][0]) + "=" + params[i][1]);

    return formattedParams.join("&");
}