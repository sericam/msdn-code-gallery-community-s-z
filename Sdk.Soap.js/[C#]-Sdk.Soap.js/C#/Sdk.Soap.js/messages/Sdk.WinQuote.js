// =====================================================================
//  This file is part of the Microsoft Dynamics CRM SDK code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// =====================================================================
"use strict";
(function ()
{
 this.WinQuoteRequest = function (quoteClose, status) {
  ///<summary>
  /// Contains the data that is needed to set the state of a quote to Won.
  ///</summary>
  ///<param name="quoteClose"  type="Sdk.Entity">
  /// Sets the quote close activity associated with this state change. Required.
  ///</param>
  ///<param name="status"  type="Number">
  /// Sets a new status of the quote. Required.
  ///</param>
  if (!(this instanceof Sdk.WinQuoteRequest)) {
   return new Sdk.WinQuoteRequest(quoteClose, status);
  }
  Sdk.OrganizationRequest.call(this);

  // Internal properties
  var _quoteClose = null;
  var _status = null;

  // internal validation functions

  function _setValidQuoteClose(value) {
   if (value instanceof Sdk.Entity) {
    _quoteClose = value;
   }
   else {
    throw new Error("Sdk.WinQuoteRequest QuoteClose property is required and must be a Sdk.Entity.")
   }
  }

  function _setValidStatus(value) {
   if (typeof value == "number") {
    _status = value;
   }
   else {
    throw new Error("Sdk.WinQuoteRequest Status property is required and must be a Number.")
   }
  }

  //Set internal properties from constructor parameters
  if (typeof quoteClose != "undefined") {
   _setValidQuoteClose(quoteClose);
  }
  if (typeof status != "undefined") {
   _setValidStatus(status);
  }

  function getRequestXml() {
   return ["<d:request>",
           "<a:Parameters>",

             "<a:KeyValuePairOfstringanyType>",
               "<b:key>QuoteClose</b:key>",
              (_quoteClose == null) ? "<b:value i:nil=\"true\" />" :
              ["<b:value i:type=\"a:Entity\">", _quoteClose.toValueXml(), "</b:value>"].join(""),
             "</a:KeyValuePairOfstringanyType>",

             "<a:KeyValuePairOfstringanyType>",
               "<b:key>Status</b:key>",
              (_status == null) ? "<b:value i:nil=\"true\" />" :
              ["<b:value i:type=\"a:OptionSetValue\">",
               "<a:Value>", _status, "</a:Value>",
              "</b:value>"].join(""),
             "</a:KeyValuePairOfstringanyType>",

           "</a:Parameters>",
           "<a:RequestId i:nil=\"true\" />",
           "<a:RequestName>WinQuote</a:RequestName>",
         "</d:request>"].join("");
  }

  this.setResponseType(Sdk.WinQuoteResponse);
  this.setRequestXml(getRequestXml());

  // Public methods to set properties
  this.setQuoteClose = function (value) {
   ///<summary>
   /// Sets the quote close activity associated with this state change. Required.
   ///</summary>
   ///<param name="value" type="Sdk.Entity">
   /// The quote close activity associated with this state change.
   ///</param>
   _setValidQuoteClose(value);
   this.setRequestXml(getRequestXml());
  }

  this.setStatus = function (value) {
   ///<summary>
   /// Sets a new status of the quote. Required.
   ///</summary>
   ///<param name="value" type="Number">
   /// A new status of the quote.
   ///</param>
   _setValidStatus(value);
   this.setRequestXml(getRequestXml());
  }

 }
 this.WinQuoteRequest.__class = true;

 this.WinQuoteResponse = function (responseXml) {
  ///<summary>
  /// Response to WinQuoteRequest
  ///</summary>
  if (!(this instanceof Sdk.WinQuoteResponse)) {
   return new Sdk.WinQuoteResponse(responseXml);
  }
  Sdk.OrganizationResponse.call(this)

  // This message returns no values
 }
 this.WinQuoteResponse.__class = true;
}).call(Sdk)

Sdk.WinQuoteRequest.prototype = new Sdk.OrganizationRequest();
Sdk.WinQuoteResponse.prototype = new Sdk.OrganizationResponse();
