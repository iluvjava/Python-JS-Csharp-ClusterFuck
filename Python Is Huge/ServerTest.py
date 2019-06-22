from flask import Flask
app = Flask(__name__)

@app.route("/")
def test_route():
    return "<h>Test</>"

@app.route("user/<username>")
def user_name():
    return "<h>Under Construction</h>"

if __name__ == "__main__":
    app.run(host="0.0.0.0", port = 8888, debug=True)