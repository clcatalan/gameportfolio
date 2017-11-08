import java.util.LinkedList;


public class State {
	
	public int[] board;
	public int turn;
	
	//Constructor for state
	public State(int[] board, int turn){
		/*If the passed board (aka values) is null, it means 
			it is the state is at its initial point (9 unmarked tiles)*/
		if(board == null){
			this.board = new int[9];
			for(int i = 0; i < 9; i++)
				this.board[i] = -1;
		}
		//Otherwise, just set this board according to the passed array
		else{
			this.board = board;
		}
		
		//Takes note of the current note of this state
		this.turn = turn;
	}
	
	//Perform a move, given an index in the board; this returns the next state (aka the state after the action)
	public State doMove(int idx){
		//Clone, first, the current state of the board
		int[] newBoard = board.clone();
		//Mark the specified index (with either 1 or 0, depending on the turn)
		newBoard[idx] = turn;
		
		//Return the next state and the next turn (1 -> 0 || 0 -> 1)
		return new State(newBoard, turn == 0 ? 1 : 0);
	}
	
	//Returns a linked list of the available moveset for the AI
	public LinkedList<Integer> availableMoves(){
		LinkedList<Integer> available = new LinkedList<Integer>();
		//Checks each index if it is vacant (hence, available) [note: -1 signifies a vacant space]
		for(int i = 0; i < 9; i++){
			if(board[i] == -1){
				available.add(i);
			}
		}
		return available;
	}
	
	//Checks if the state is already finished (someone has won)
	public boolean checkGoalState(int turn){
		/*For visualization, the board can be imagined like this:
		 * [0][1][2]
		 * [3][4][5]
		 * [6][7][8]
		 */
		//Check if 3-in-a-row: column-based
		if(board[0] == board[3] && board[3] == board[6] && board[0] == turn) return true;	//1st column
		if(board[1] == board[4] && board[4] == board[7] && board[1] == turn) return true;	//2nd column
		if(board[2] == board[5] && board[5] == board[8] && board[2] == turn) return true;	//3rd column
		
		//Checks if 3-in-a-row: row-based
		if(board[0] == board[1] && board[1] == board[2] && board[0] == turn) return true;	//1st column
		if(board[3] == board[4] && board[4] == board[5] && board[3] == turn) return true;	//2nd column
		if(board[6] == board[7] && board[7] == board[8] && board[6] == turn) return true;	//3rd column
		
		//Checks if 3-in-a-row: diagonally
		if(board[0] == board[4] && board[4] == board[8] && board[0] == turn) return true;	//diagonal 1
		if(board[2] == board[4] && board[4] == board[6] && board[2] == turn) return true;	//diagonal 2
		
		//If it did not enter a single condition above, return false
		return false;
	}
	
	//Returns a boolean if the game has already ended (by checking if the state is in the goal state)
		public boolean gameEnd(){
			
			return checkGoalState(0) || checkGoalState(1) || availableMoves().size() == 0;
		}
	
	
	/* -AI algorithm for determining the next best move using minimax-
	 * Dex: I've initially coded this with the user always having the first turn hence,
	 * the AI's main priority is to block the user. However, when I've included
	 * the possiblity that the AI takes the first turn, this code surprisingly 
	 * still worked (the AI's priority, in this case, is to win).
	 * */
	public int minimax(){
		//First, checks if the state is already finished
		//Checks the goal state with respect to turn 0 ('X')
		if(checkGoalState(0))	//If 'X' has won
			return 10;	
		if(checkGoalState(1))	//If 'O' has won
			return -10;
		if(availableMoves().isEmpty())	//If draw
			return 0;
		
		int score = Integer.MIN_VALUE;
		//Perform minimax to each available move
		for(int i = 0; i < availableMoves().size(); i++){
			int idx = availableMoves().get(i);
			int value = doMove(idx).minimax();	//the score achieved by performing the move
			//Update the score
			if(score == Integer.MIN_VALUE || (turn == 0 && score < value) || (turn == 1 && value < score)){
				score = value;
			}
		}
		
		//Return score; note that I've subtracted/added 1 because of the excess move
		return score + (turn == 0 ? -1 : 1);
	}
	
	//Returns the index of the best move
	public int findBestMove(){
		int score = Integer.MIN_VALUE;
		int best = -1;	//index
		
		//For each available move, "simulate" that move
		for(int i = 0; i < availableMoves().size(); i++){
			int idx = availableMoves().get(i);
			int value = doMove(idx).minimax();
			//If the applied move resulted to a win state, then that is the best move (obviously)
			if(doMove(idx).checkGoalState(turn)) return idx;
			//If it didn't result to a winning state, find the best move depending on the minimax value
			if(score == Integer.MIN_VALUE || turn == 0 && score < value || turn == 1 && value < score){
				score = value;
				best = idx;
			}
		}
		
		return best;
	}
	
}
