import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-guessing-game',
  templateUrl: './guessing-game.component.html',
  styleUrls: ['./guessing-game.component.scss']
})
export class GuessingGameComponent implements OnInit {

  deviation: number;
  noOfTries: number;
  original: number;
  guess: number;

  constructor() {
  }

  initializeGame() {
    this.noOfTries = 0;
    this.original = Math.floor(Math.random() * 1000 + 1);
    this.guess = null;
    this.deviation = null;
  }
  verifyGuess() {
    this.deviation = this.original - this.guess;
    this.noOfTries = this.noOfTries + 1;
  }

  ngOnInit() {
    this.initializeGame();
  }

}
