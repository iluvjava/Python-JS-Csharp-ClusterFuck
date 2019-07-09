# ------------------------------------------------------------------------------
"""
This script is a simple website in python flask.
"""

# -------------------------------------------------------------------------------
from flask import Flask, render_template, send_file, url_for, request
import ContentPreparation as Prep

app = Flask(__name__)


@app.route("/")
def test_route():
    return "<h>Test</>"


@app.route("/user/<username>")
def user_name():
    return "<h>Under Construction</h>"


@app.route("/specific")
def specific_website():
    '''
        Render a specific website that use the jinja template to 
        render the content of the webpage. 
    '''
    thelist = Prep.Instance.get_books()
    booknamelink = []

    if len(booknamelink) == 0:
        return render_template("specificswebsite.html")

    for bookname in thelist:
        booknamelink.append(
            (bookname,
             url_for("file_access", filename=bookname)
             )
        )
    return render_template("specificwebsite.html", elementlist=booknamelink)


@app.route("/bunchofpdfs/<filename>", methods=["GET"])
def file_access(filename):
    """
        This function returns a file attechment to the client
        return: 
            a file in the static/resource/bunchofpdfs/
    """
    filepath = "static/resource/bunchofpdfs/" + filename
    return send_file(filepath, mimetype="application/pdf")


@app.route("/postportal", methods =["Post"])
def post_portal():
    '''
    Post test.
    :param parameters:
    :return:
    '''
    parameters = request.form.to_dict()

    return str(parameters)


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=8888, debug=True)
