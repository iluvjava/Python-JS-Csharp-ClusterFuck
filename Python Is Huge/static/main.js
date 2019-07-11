(()=>
{
    "use strict";

    window.addEventListener("load", initiate);
    const ApiEndpoint1 = "../getvideos";

    function initiate()
    {
        console.log("main.js script is running.");
        id("videobtn").addEventListener("click", videoBtnEvent);
    }

    /**
        Puplate the video using a post request.
        - Post request.
        -
    */
    function videoBtnEvent(event)
    {
        let formData = new FormData();
        formData.append("videos", "");
        fetch(ApiEndpoint1,
             {method:"POST", body:formData}
            )
        .then(checkStatus).then(fetchStuff).catch(console.log);
    }

    /**
        Function handles the return from the API calls.
        - Get the first key pair element from the json, it should be a video name.
        @params resp
            The json response from the POST api.
        @param
    */
    function fetchStuff(resp)
    {
        console.log("Post response text: ");
        console.log(resp);
        let myjson = JSON.parse(resp);
        let keyset = Object.keys(myjson);

        // PrepreTitle:
        {
            let title = document.createElement("h3");
            title.innerText = keyset[0];
            id("vidframe").appendChild(title);
        }
        //Prepare video tag
        {
            let src = document.createElement("source");
            src.src = myjson[keyset[0]];
            src.type = myjson["type"];
            let vidtag = document.createElement("video");
            vidtag.appendChild(src);
            vidtag.setAttribute("controls","");
            id("vidframe").appendChild(vidtag);
        }
        return resp;
    }

    /**
        Check status from the promise.
    */
    function checkStatus(proms)
    {
        if(proms.status == 200)
        {
            console.log("status: ok");
        }
        return proms.text();
    }

    /**
    *
    */
   function qs(str)
   {
        return document.querySelector(str);
   }

    /**
    *
    */
   function qsa(str)
   {
        return document.querySelectorAll(str);
   }

    /**
    *
    */
   function id(arg)
   {
        return document.getElementById(arg)
   }

})();