from flask import Flask, render_template

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
    elementlist = ["element 1", "element 2"]

    return render_template("specificwebsite.html", elementlist = elementlist)



if __name__ == "__main__":
    app.run(host="0.0.0.0", port=8888, debug=True)