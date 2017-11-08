import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;


public class Button extends JButton implements ActionListener {
	int color;
	int x, y;
	public Button(){
		
		this.addActionListener(this);			
	}
	
	

	@Override
	public void actionPerformed(ActionEvent e) {
		// TODO Auto-generated method stub
		State state = null;
		if(e.getSource()==this){
			int a, b;
			a=this.x;
			b=this.y;
			mainClass.changeCells(a, b);	//center
			mainClass.changeCells(a, b+1); //right		
			mainClass.changeCells(a, b-1); //left
			mainClass.changeCells(a+1, b); //bottom
			mainClass.changeCells(a-1, b); //top
			mainClass.goalTest(mainClass.getState(state));
			
		}
		
	}
}
