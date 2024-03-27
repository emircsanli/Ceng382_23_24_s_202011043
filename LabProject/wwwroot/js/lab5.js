function HideElements(){

var element1 = document.getElementById("myCarousel");
element1.style.display = "none";

var element2 = document.querySelectorAll('p');
element2.forEach(function(element) {
    element.style.display = 'none';
  });
  var element3 = document.querySelectorAll('h1');
element3.forEach(function(element) {
    element.style.display = 'none';
  });
  var element4 = document.querySelectorAll('h2');
  element4.forEach(function(element) {
      element.style.display = 'none';
    });

  var hideButton = document.getElementById('toggleElementButton');
  hideButton.innerHTML = "Show elements";
  hideButton.setAttribute('onclick', 'ShowElements()');
}

function ShowElements(){

var element1 = document.getElementById("myCarousel");
element1.style.display = 'block';

var element2 = document.querySelectorAll('p');
element2.forEach(function(element) {
    element.style.display = 'block';
  });
  var element3 = document.querySelectorAll('h1');
element3.forEach(function(element) {
    element.style.display = 'block';
  });
  var element4 = document.querySelectorAll('h2');
  element4.forEach(function(element) {
      element.style.display = 'block';
    });
    
  var showButton = document.getElementById('toggleElementButton');
  showButton.innerHTML = "Hide Elements";
  showButton.setAttribute('onclick', 'HideElements()');
}

function HideAdditionForm(){
  var element1 = document.getElementById("additionForm");
element1.style.display = "none";

var hideButton = document.getElementById('toggleAdditionButton');
  hideButton.innerHTML = "Show Addition Form";
  hideButton.setAttribute('onclick', 'ShowAdditionForm()');
  
  var realSum = document.getElementById('answer');
    realSum.style.display = 'none';
}

function ShowAdditionForm(){
  var element1 = document.getElementById("additionForm");
  element1.style.display ='block';

  var showButton = document.getElementById('toggleAdditionButton');
  showButton.innerHTML = "Hide Addition Form";
  showButton.setAttribute('onclick', 'HideAdditionForm()');
}

function calculate(){

  var firstNumber = parseInt(document.getElementById('firstNumber').value);
  var secondNumber = parseInt(document.getElementById('secondNumber').value);

  var sum= firstNumber+secondNumber;

  var realSum = document.getElementById('answer');
  realSum.style.display = 'block';
  document.getElementById('answer').innerHTML = "The sum of "+firstNumber+" and "+secondNumber+" is: "+sum;
}