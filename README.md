# Woz.Linq

A portabe class with extra Linq functions that I feel are missing from the standard MS implementation with support for:
- .NET Framework 4.5
- .NET Framework 4.5.1
- .NET Framework 4.6
- ASP.NET Core 5
- Windows Universal 10
- Windows 8
- Windows Phone 8.1
- Windows Phone Silverlight 8

## Background

While this might appear to be a new project it is a compilation of various Linq extras I have created over the years.

## Extras for IEnumberable

Functionality
- ToEnumerable = Wrap a value in an IEnumerable 
- Prepend = Add a value at the head of an IEnumberable
- Concat = Add a value to the end of an IEnumberable
- MinOrElse = Get the Min of an IEnumberable or return a default when empty
- MaxOrElse = Get the Max of an IEnumberable or return a default when empty
- MinBy = Get the element with the Min value identified by a selector
- MinByOrElse = Get the element with the Min value identified by a selector or run element factory when empty
- MaxBy = Get the element with the Max value identified by a selector
- MaxByOrElse = Get the element with the Max value identified by a selector or run element factory when empty
- Each = Foreach but lambda based

## Extras for IEnumerator and IEnumerator&lt;T&gt;

Functionality
- ToArray = Read the IEnumerator and buffer into an array
- ToEnumerable = Convert the IEnumerator into and IEnumberable
= Select = Run a selector over the IEnumerator and return as an IEnumberable