(function() {
  var app = angular.module('NChancellors', [ ]);
  
  app.controller('PuzzleController', function() {
    var ctrl = this;
    ctrl.n = 0;
    
    ctrl.imgURL = "images/box.png";
    ctrl.unavailableURL = "images/coal.png";
    ctrl.chancellorURL = "images/present.png";
    ctrl.blankURL = "images/box.png";
    ctrl.cells = new Array();
    ctrl.ansCtr = 0;
    ctrl.solutions = new Array();
    ctrl.gactrlEnd = false;
    var audio = new Audio('win.wav');

    var TRUE = 1;
    var FALSE = 0;

    var print_solution = function(n,x, title) {
        var i,j;
        
        var solution = new Array(n+1);
        for (i = 0; i < n+1; i++) {
          solution[i] = new Array(n+1);
        }
        
        // pre-fill with dashes
        for(i=1;i<=n;i++) {
            for(j=1; j<=n; j++) {
                solution[i][j]='-';
            }
        }
        
        // fill solutions
        for(i=1;i<=n;i++) {
            solution[i][x[i]]='C';
        }
        
        // print table
        var str = title;
        for( i=1;i<=n;i++) {
            for(j=1;j<=n;j++) {
              str = str + " " + solution[i][j];
                //console.log("\t" + solution[i][j]);
            }
            str = str + " \n";
            //str = "";
        }
        console.log(str);
        ctrl.solutions.push(str);
    };


    var place = function(x,k) {
        var i;
        for(i=1;i<k;i++) { // compare against previous answers
    
            if (x[i]==x[k]) { // if sactrl column
                return FALSE;
            }
    
            if (i-x[i]==k-x[k]) { // if sactrl back-diagonal
                //return FALSE;
            }
    
            if (i+x[i]==k+x[k]) { // if sactrl forward-diagonal
                //return FALSE;
            }
            // knight moves
            if (i==k-1 && x[i]==x[k]-2) { // col-2, row-1
                return FALSE;
            }
            if (i==k+1 && x[i]==x[k]-2) { // col-2, row+1
                return FALSE;
            }
            if (i==k-1 && x[i]==x[k]+2) { // col+2, row-1
                return FALSE;
            }
            if (i==k+1 && x[i]==x[k]+2) { // col+2, row+1
                return FALSE;
            }
            if (i==k-2 && x[i]==x[k]-1) { // col-1, row-2
                return FALSE;
            }
            if (i==k+2 && x[i]==x[k]-1) { // col-1, row+2
                return FALSE;
            }
            if (i==k-2 && x[i]==x[k]+1) { // col+1, row-2
                return FALSE;
            }
            if (i==k+2 && x[i]==x[k]+1) { // col+1, row+2
                return FALSE;
            }
        }
    
        return TRUE;
    };
    
    var nchancellors = function(puzzle, n, initial) {
        var x = new Array(n+1); // queen positions: row=index, column=value
        var count=0;
        var k=1;
    
        x[k]=0;
        
        while(k!=0) {
            x[k]=x[k]+1;
            while((x[k]<=n)&&(!place(x,k))) { // try each column to place queen
                x[k]=x[k]+1; // if cannot be placed in column x[k], try next column (x[k]+1)
            }
            if(x[k]<=n) {
                if(k==n) { // n queens have been placed
                    var i, hasInitialSolution = TRUE;
                    for (i=1; i<=n; i++) {
                        if (initial[i]>0 && initial[i]!=x[i]) {
                            hasInitialSolution = FALSE;
                        }
                    }
                    if (hasInitialSolution) {
                        count++;
                        var title = "\nSolution "+count+": \n";
                        print_solution(n,x, title);
                    }
                } else { // next queenE
                    k++;
                    x[k]=0;
                }
            } else { //  backtrack!
                k--;
            }
        }
    
        if (count==0) {
            //console.log("\n\tPuzzle " + puzzle + " has no solutions\n");
        }
        
        
        return;
    };
    
    this.main = function(){
      
      if (ctrl.cells.length > 0) {
        ctrl.cells = new Array();
        ctrl.solutions = new Array();
        ctrl.gactrlEnd = false;
        ctrl.ansCtr = 0;
        return;
      }
      
      var puzzles = 1;
      var n = ctrl.n;
      
      var initial = new Array(n+1);
      for (var i = 0; i < n+1; i++) {
        initial[i] = 0;
      }
      
      var row = 1;
      var col = 1;
      for(var i = 0; i < n*n; i++){
        var cell = {
          'row' : row,
          'col' : col,
          'isEnd' : false,
          'url' : ctrl.blankURL,
          'aggressors' : 0,
          'toggleImage' : function () {
              if (this.url === ctrl.blankURL) {
                  this.url = ctrl.chancellorURL;
              } else {
                  this.url = ctrl.blankURL;
              }
          },
          'disable' : function() { 
            for(var i = 0; i < ctrl.cells.length; i++){
              if (this.row == ctrl.cells[i].row && this.col == ctrl.cells[i].col) { 
                
              } else if (this.row == ctrl.cells[i].row || this.col == ctrl.cells[i].col) {        
                ctrl.cells[i].url = ctrl.unavailableURL;
                ctrl.cells[i].aggressors++; //track the number of coal cells, for enable function
              }
              else if (this.row-2 == ctrl.cells[i].row && this.col-1 == ctrl.cells[i].col) {
                ctrl.cells[i].url = ctrl.unavailableURL;
                ctrl.cells[i].aggressors++;
              }
              else if (this.row-2 == ctrl.cells[i].row && this.col+1 == ctrl.cells[i].col) {
                ctrl.cells[i].url = ctrl.unavailableURL;
                ctrl.cells[i].aggressors++;
              }
              else if (this.row-1 == ctrl.cells[i].row && this.col-2 == ctrl.cells[i].col) {
                ctrl.cells[i].url = ctrl.unavailableURL;
                ctrl.cells[i].aggressors++;
              }
              else if (this.row-1 == ctrl.cells[i].row && this.col+2 == ctrl.cells[i].col) {
                ctrl.cells[i].url = ctrl.unavailableURL;
                ctrl.cells[i].aggressors++;
              }
              else if (this.row+1 == ctrl.cells[i].row && this.col-2 == ctrl.cells[i].col) {
                ctrl.cells[i].url = ctrl.unavailableURL;
                ctrl.cells[i].aggressors++;
              }
              else if (this.row+1 == ctrl.cells[i].row && this.col+2 == ctrl.cells[i].col) {
                ctrl.cells[i].url = ctrl.unavailableURL;
                ctrl.cells[i].aggressors++;
              }
              else if (this.row+2 == ctrl.cells[i].row && this.col-1 == ctrl.cells[i].col) {
                ctrl.cells[i].url = ctrl.unavailableURL;
                ctrl.cells[i].aggressors++;
              }
              else if (this.row+2 == ctrl.cells[i].row && this.col+1 == ctrl.cells[i].col) {
                ctrl.cells[i].url = ctrl.unavailableURL;
                ctrl.cells[i].aggressors++;
              }
            }
          },
          'enable' : function() {
            for(var i = 0; i < ctrl.cells.length; i++){
              if (this.row == ctrl.cells[i].row && this.col == ctrl.cells[i].col) {
                
              } else if (this.row == ctrl.cells[i].row || this.col == ctrl.cells[i].col) {        
                ctrl.cells[i].aggressors--;
                if (ctrl.cells[i].aggressors == 0) {
                  ctrl.cells[i].url = ctrl.blankURL;
                }
              }
              // horse
              else if (this.row-2 == ctrl.cells[i].row && this.col-1 == ctrl.cells[i].col) {
                ctrl.cells[i].aggressors--;
                if (ctrl.cells[i].aggressors == 0) {
                  ctrl.cells[i].url = ctrl.blankURL;
                }
              }
              else if (this.row-2 == ctrl.cells[i].row && this.col+1 == ctrl.cells[i].col) {
                ctrl.cells[i].aggressors--;
                if (ctrl.cells[i].aggressors == 0) {
                  ctrl.cells[i].url = ctrl.blankURL;
                }
              }
              else if (this.row-1 == ctrl.cells[i].row && this.col-2 == ctrl.cells[i].col) {
                ctrl.cells[i].aggressors--;
                if (ctrl.cells[i].aggressors == 0) {
                  ctrl.cells[i].url = ctrl.blankURL;
                }
              }
              else if (this.row-1 == ctrl.cells[i].row && this.col+2 == ctrl.cells[i].col) {
                ctrl.cells[i].aggressors--;
                if (ctrl.cells[i].aggressors == 0) {
                  ctrl.cells[i].url = ctrl.blankURL;
                }
              }
              else if (this.row+1 == ctrl.cells[i].row && this.col-2 == ctrl.cells[i].col) {
                ctrl.cells[i].aggressors--;
                if (ctrl.cells[i].aggressors == 0) {
                  ctrl.cells[i].url = ctrl.blankURL;
                }
              }
              else if (this.row+1 == ctrl.cells[i].row && this.col+2 == ctrl.cells[i].col) {
                ctrl.cells[i].aggressors--;
                if (ctrl.cells[i].aggressors == 0) {
                  ctrl.cells[i].url = ctrl.blankURL;
                }
              }
              else if (this.row+2 == ctrl.cells[i].row && this.col-1 == ctrl.cells[i].col) {
                ctrl.cells[i].aggressors--;
                if (ctrl.cells[i].aggressors == 0) {
                  ctrl.cells[i].url = ctrl.blankURL;
                }
              }
              else if (this.row+2 == ctrl.cells[i].row && this.col+1 == ctrl.cells[i].col) {
                ctrl.cells[i].aggressors--;
                if (ctrl.cells[i].aggressors == 0) {
                  ctrl.cells[i].url = ctrl.blankURL;
                }
              }
            }
          },
          'onClick' : function() {
            if (ctrl.gactrlEnd) {
              return;
            }
            if (this.url == ctrl.blankURL) {
              this.toggleImage(); //toggle image
              this.disable(); //disables other buttons
              ctrl.ansCtr++; 
            } else if (this.url == ctrl.chancellorURL) {
              this.toggleImage(); //toggle image back into blank
              this.enable(); //enable buttons that were disabled
              ctrl.ansCtr--;
            }
            if (ctrl.ansCtr == ctrl.n) { //condition for winning
              ctrl.gactrlEnd = true;
              audio.play();
              alert("YOU'VE SAVED CHRISTMAS!");
            }
          }
        };
        col++;
        if ((i+1)%n==0) {
          cell.isEnd = true;
          row++;
          col = 1;
        }
        ctrl.cells.push(cell);
      }
      
    };
    
    ctrl.solve = function(){
      
      if (ctrl.solutions.length > 0 || ctrl.cells.length == 0 || ctrl.gactrlEnd) {
        return;
      }
      
      var initial = new Array(ctrl.n + 1);
      for (var i = 0; i < initial.length; i++) {
        initial[i] = 0;
      }
      
      var row = 1;
      var col = 1;
      var ctr = 0;
      for (var i = 0; i < ctrl.cells.length; i++) {
        if (ctrl.cells[i].url == ctrl.chancellorURL) {
          initial[row] = col;
        }
        if(col > ctrl.n-1){
            row++;
            col = 0;
        }
        col++;
        ctr++;
      }
      
      nchancellors(1, ctrl.n, initial);
      ctrl.gactrlEnd = true;
    };

  });
  
  
})();