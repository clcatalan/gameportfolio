import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;

import javax.swing.JButton;
public class Tile extends JButton{
	
	//The tile's ID
	public int tileID;
	
	//Constructor for the Tile class; basically a modified JButton
	public Tile(int tileID){
		this.tileID = tileID;
		this.setText("");
		this.setPreferredSize(new Dimension(150,150));
		this.setBackground(Color.WHITE);
		this.setFont(new Font(null, Font.PLAIN, 100));
	}
	
	//To be called every turn (to mark an empty tile)
	public void setTileCharacter(int i){
		//If X's turn
		if(i == 0) this.setText("X");
		//If O's turn
		else this.setText("O");
	}

}
