from flask import Flask, render_template
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
    thelist = Prep.prepare_list()
    return render_template("specificwebsite.html", elementlist=thelist)

@app.route("/bunchofpdfs/<filename>", methods=["GET"])
def file_access(filename):
    """
        This function returns a file attechment to the client
        return: 
            a file in the static/resource/bunchofpdfs/
    """


    return "getrequest paremeter: " + filename


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=8888, debug=True)