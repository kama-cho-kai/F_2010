from flask import Flask, request, jsonify

app = Flask(__name__)
app.config["JSON_AS_ASCII"] = False  # json形式をascii以外で扱う


@app.route("/", methods=["POST"])
def text_reaction():
    request_json = request.get_json()
    return jsonify({"message": request_json["text"]}), 200


if __name__ == "__main__":
    app.run(host="127.0.0.1", port="8080", debug=True)
