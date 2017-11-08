import java.awt.Dimension;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JFrame;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.SwingUtilities;


public class TicTacToe extends JPanel implements ActionListener{
	private State state = new State(null, 0);
	private Tile[] tiles = new Tile[9];
	private boolean goFirst;
	
	//Constructor with 1 parameter -- if the user wants to go first
	public TicTacToe(){
		
		//Asks the user if he/she wants to go first
		int result = JOptionPane.showConfirmDialog(null, 
			"Do you want to go first?", 
			"Do you want to go first?", 
			JOptionPane.YES_NO_OPTION);
		this.goFirst = (result == JOptionPane.YES_OPTION ? true : false);
		
		//Sets the panel's layout
		this.setLayout(new GridLayout(3,3));
		
		//Create 9 empty tiles and add them to the board
		for(int i = 0; i < 9; i++){
			Tile button = new Tile(i);
			tiles[i] = button;
			button.addActionListener(this);
			this.add(button);
		}
		
		//If the AI has to go first, it chooses the middle one -- since it's the best option, right? 
		if(!goFirst){
			tiles[4].setTileCharacter(state.turn);
			move(4);
			tiles[4].setEnabled(false);
		}
	}
	
	//Do a move, given an index of the tile
	private void move(int idx){
		//State's move returns a new state (aka the state after the move)
		state = state.doMove(idx);
	}
	
	//ActionListener for each tile
	public void actionPerformed(ActionEvent e) {
		//Gets the tile that invoked the "click action"
		Tile tile = (Tile)e.getSource();
		int tileID = tile.tileID;
		
		if(tile.isEnabled()){
			//Mark the tile depending on the current turn (current player)
			tile.setTileCharacter(state.turn);
			
			//Updates the state of the game
			move(tileID);
			
			//Every turn, checks the state if it's in the goal state
			//The condition below is when the game is not finished yet, hence the AI's turn
			if(!state.gameEnd()){
				//Get the index/tileID of the best move, based from the minimax algorithm
				int best = state.findBestMove();
				tiles[best].setTileCharacter(state.turn);	//Marks the tile with the AI's symbol(O or X)
				move(best);	//Updates the current state of the game
				tiles[best].setEnabled(false);
			}
			
			//The condition below is when the game is finished
			if(state.gameEnd()){
				String message = "";
				
				if(state.checkGoalState(0))
					message = (goFirst ? "You win!" : "You lose!");
				else if(state.checkGoalState(1))
					message = (goFirst ? "You lose!" : "You win!");
				else message = "Draw game!";

				int result = JOptionPane.showConfirmDialog(null, 
						message + "\nDo you want to go again?", message, JOptionPane.YES_NO_OPTION);
				if(result == JOptionPane.YES_OPTION){
					resetGame();
					return;
				}
				else{
					System.exit(0);
				}
			}
			tile.setEnabled(false);
		}

	}
	
	private void resetGame(){
		state = new State(null, 0);
		//"Resets" each tile
		for(int i = 0; i < 9; i++){
			tiles[i].setEnabled(true);
			tiles[i].setText("");
		}
		//"Asks again if the user wants to go first
		int result = JOptionPane.showConfirmDialog(null, 
			"Do you want to go first?", 
			"Do you want to go first?", 
			JOptionPane.YES_NO_OPTION);
		this.goFirst = (result == JOptionPane.YES_OPTION ? true : false);
		
		//If not...
		if(!goFirst){
			tiles[4].setTileCharacter(state.turn);
			move(4);
			tiles[4].setEnabled(false);
		}
		
	}
	
	//Main Function
	public static void main(String[] args) {
		JFrame frame = new JFrame("Tic-Tac-Toe");
		TicTacToe game = new TicTacToe();
		frame.add(game);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.pack();
		frame.setVisible(true);

	}

}
