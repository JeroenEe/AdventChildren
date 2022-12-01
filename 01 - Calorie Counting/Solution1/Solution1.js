const { readFileSync } = require("fs");

let input = readFileSync("../input.txt", "utf-8")
  .split("\n")
  .map((value) => value.trimEnd());

let totals = [];
let temp = 0;

for (let i of input) {
  if (i == "") {
    totals.push(temp);
    temp = 0;
  } else {
    temp += parseInt(i);
  }
}

let result = totals
  .sort((a, b) => a - b)
  .slice(-3)
  .reduce((a, b) => a + b);
console.log(result);
