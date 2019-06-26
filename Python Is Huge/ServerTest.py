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
    return render_template("specificwebsite.html")
@app.rounte()
def script()

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=8888, debug=True)