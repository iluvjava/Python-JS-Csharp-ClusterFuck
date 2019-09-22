/*
 * CSE 154 Summer 2019
 * Node.JS intro
 *
 * Starter code for making requests to your new API!
 */

"use strict";
(function() {

  window.addEventListener("load", init);

  /**
   * Setup event listeners for request buttons.
   */
  function init() {
    id("hello-btn").addEventListener("click", requestHello);
    id("name-btn").addEventListener("click", requestName);
    id("circle-btn").addEventListener("click", requestCircle);
    id("rectangle-btn").addEventListener("click", requestRectangle);
    id("power-btn").addEventListener("click", requestPower);
  }

  /**
   * Makes a request to /hello
   */
  function requestHello() {
    // TODO: Make a request to this endpoint!
  }

  /**
   * Makes a request to /hello/name?name=name
   */
  function requestName() {
    let firstName = id("firstname-in").value;
    let lastName = id("lastname-in").value;

    // TODO: Make a request to this endpoint!
  }

  /**
   * Makes a request to /math/circle/:r
   */
  function requestCircle() {
    let radius = id("radius-in").value;

    // TODO: Make a request to this endpoint!
  }

  /**
   * Makes a request to /math/rectangle/:width/:height
   */
  function requestRectangle() {
    let width = id("width-in").value;
    let height = id("height-in").value;

    // TODO: Make a request to this endpoint!
  }

  /**
   * Makes a request to /math/:base^:exponent
   */
  function requestPower() {
    let base = id("base-in").value;
    let exponent = id("exponent-in").value;

    // TODO: Make a request to this endpoint!
  }

  /**
   * Helper function to return the response's result text if successful, otherwise
   * returns the rejected Promise result with an error status and corresponding text
   * @param {object} response - response to check for success/error
   * @return {object} - valid response if response was successful, otherwise rejected
   *                    Promise result
   */
  function checkStatus(response) {
    if (!response.ok) {
      throw Error("Error in request: " + response.statusText);
    }
    return response; // a Response object
  }

  /**
   * Returns the element that has the ID attribute with the specified value.
   * @param {string} id - element ID
   * @return {object} DOM object associated with id.
   */
  function id(id) {
    return document.getElementById(id);
  }

})();
