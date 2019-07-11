# ------------------------------------------------------------------------------
"""
This script is a simple website in python flask.
"""

# -------------------------------------------------------------------------------
from flask import Flask, render_template, send_file, url_for, request, json, Response
import ContentPreparation as Prep

app = Flask(__name__)


@app.route("/")
def test_route():
    return "<h>Test</>"


@app.route("/user/<username>")
def user_name():
    return "<h>Under Construction</h>"


@app.route("/index")
def specific_website():
    '''
        Render a specific website that use the jinja template to 
        render the content of the webpage. 
    '''
    BookList = Prep.BooksShelveInstance.get_books()
    BookLinksList = []
    if len(BookList) == 0:
        return render_template("specificwebsite.html")
    for bookname in BookList:
        BookLinksList.append(
            (bookname,
             url_for("file_access", filename=bookname, accesstype="Books")
             )
        )
    return render_template("specificwebsite.html", elementlist=BookLinksList)


@app.route("/files/<filename>", methods=["GET"])
def file_access(filename):
    """
        This function returns a file attechment to the client
        :return:
            a file in the static/resource/bunchofpdfs/
        :param
            accesstype: each string corresponds to a different implementation.
    """
    filepath = None
    ContentType = None
    accesstype = request.args.get("accesstype")
    if (accesstype == "Books"):
        filepath = Prep.BooksShelveInstance.get_rootdir() + "/" + filename
        ContentType = "application/pdf"
    elif (accesstype == "Videos"):
        filepath = Prep.VideoShelvesInstance.get_abs_rootdir() + "/" + filename
        ContentType = "video/mp4"
    return send_file(filepath, mimetype=ContentType)


@app.route("/postportaltest", methods=["Post"])
def post_portal():
    '''
        Constructing a response to post request, responding with a json type.
    :param parameters:
    :return:
    '''
    parameters = request.form.to_dict()  # parameters, fromdata.
    if (len(parameters) == 0):
        parameters["status"] = "unsuccessful"
    else:
        parameters["status"] = "successful"
    js = json.dumps(parameters)  # construct the json response.
    resp = Response(js, status=200, mimetype="application/json")
    return resp


@app.route("/getvideos", methods=["Post"])
def get_video():
    """
        A post endpoint, very simple.
        simple json that: video names -> get URL
            - type -> mimetype of video
    :return:
        Json object for API
    """
    PostParams = request.form.to_dict()
    Prejs = {}
    if ("videos" not in PostParams.keys()):
        Prejs["error"] = "Invalid Post Parameters"
        return Response(Prejs, status=403, mimetype="application/json")
    VideoList = Prep.VideoShelvesInstance.get_filenames()
    for fn in VideoList:
        Prejs[fn] = url_for("file_access", filename=fn, accesstype="Videos")
    Prejs["type"] = "video/mp4";
    return Response(json.dumps(Prejs), status=200, mimetype="application/json")


@app.errorhandler(404)
def not_found(error=None):
    responses = {}
    responses["title"] = "Error page 404"
    responses["errorcode"] = 404
    responses["erromessage"] = "Page not found..."
    return render_template("errorpage.html", arg=responses)


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=8888, debug=True)
