// Copyright 2009 the Sputnik authors.  All rights reserved.
/**
 * Object.prototype.hasOwnProperty can't be used as a constructor
 *
 * @path ch15/15.2/15.2.4/15.2.4.5/S15.2.4.5_A7.js
 * @description Checking if creating "new Object.prototype.hasOwnProperty" fails
 */

var FACTORY = Object.prototype.hasOwnProperty;

try {
  instance = new FACTORY;
  $FAIL('#1: Object.prototype.hasOwnProperty can\'t be used as a constructor');
} catch (e) {
  $PRINT(e);

}

