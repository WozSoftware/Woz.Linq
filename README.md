# Woz.Linq

Now released under Unlicense (http://unlicense.org)

A compact lightweight thread safe IOC library for .NET compiled against .NETStandard 1.6

## Background

While this might appear to be a new project it is a compilation of various Linq extras I have created over the years.

## Extras for IEnumberable

Functionality
- ToEnumerable = Wrap a value in an IEnumerable 
- Concat = Add a value to the end of an IEnumberable
- MinOrElse = Get the Min of an IEnumberable or return a default when empty
- MaxOrElse = Get the Max of an IEnumberable or return a default when empty
- MinBy = Get the element with the Min value identified by a selector
- MinByOrElse = Get the element with the Min value identified by a selector or run element factory when empty
- MaxBy = Get the element with the Max value identified by a selector
- MaxByOrElse = Get the element with the Max value identified by a selector or run element factory when empty
- DistinctBy - Get the distinct elements using a selected key
- Each = Foreach but lambda based

## Extras for IEnumerator and IEnumerator&lt;T&gt;

Functionality
- ToArray = Read the IEnumerator and buffer into an array
- ToEnumerable = Convert the IEnumerator into and IEnumberable
- Select = Run a selector over the IEnumerator and return as an IEnumberable
