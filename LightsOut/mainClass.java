import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.util.LinkedList;
import java.util.Scanner;

import javax.swing.JButton;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;


public class mainClass implements ActionListener{
	JPanel lightsPanel = new JPanel();
	static Button [][] button = new Button[5][5];
	static String [][] a = new String[5][5];
	JButton reset;
	JButton solve;
	JButton rand;
	JButton load;
	LinkedList <State> frontier;
	static int loadCounter = 0;
	static Scanner x = null;
	
	
	public static void main(String[] args){ //all frame, and panel instantiations are here
		mainClass m = new mainClass();
		m.SetFrame();
		
	}

	private void SetFrame(){
		JFrame mainFrame = new JFrame();
		mainFrame.setSize(500,500); //creates the jframe
		mainFrame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		mainFrame.setTitle("Brute Force Search");
		mainFrame.setVisible(true);
		
		JPanel buttonsPanel = new JPanel();
		
		GridLayout g = new GridLayout(5,5);
		lightsPanel.setLayout(g);
		
		reset = new JButton();
		reset.setText("Reset");
		reset.addActionListener(this);
		buttonsPanel.add(reset);
		
		solve = new JButton();
		solve.setText("Solve");
		solve.addActionListener(this);
		buttonsPanel.add(solve);
		
		rand = new JButton();
		rand.setText("Rand");
		rand.addActionListener(this);
		buttonsPanel.add(rand);
		
		load = new JButton();
		load.setText("Load");
		load.addActionListener(this);
		buttonsPanel.add(load);
		
		mainFrame.add(buttonsPanel, BorderLayout.NORTH);
		
		setButtons();
		mainFrame.add(lightsPanel, BorderLayout.CENTER);
		
	

		
		
	}
	

	
	private void setButtons(){

		for(int i=0;i<5;i++){
			for(int j=0;j<5;j++){
				button[i][j] = new Button();
			//	int count = (int) (Math.random()*(1-0+1)) + 0; // *(high-low+1) will result in 1 or 0
				button[i][j].color = 1;
				button[i][j].x = i;
				button[i][j].y = j;
				setConfiguration(button[i][j]);
				lightsPanel.add(button[i][j]);
			}
		}
	}

	
	private static void setConfiguration(Button button){
		if(button.color==1){
			button.setBackground(Color.BLACK);
		}
		else if(button.color==0){
			button.setBackground(Color.WHITE);
		}
		

	}
	
	public static void changeCells(int x,int y){//function that changes the state of the button and its surroundings when clicked
		
		
		//normal changing of cells
		for(int i=0;i<5;i++){ //searches through the rows
			for(int j=0;j<5;j++){ //searches through the columns
				if(i==x&&j==y){ //once found, change the states
					if(button[i][j].color==0){ //if it is white
						button[i][j].color=1;
						setConfiguration(button[i][j]);
						
					}
					else if(button[i][j].color==1){ //if it is black
						button[i][j].color = 0;
						setConfiguration(button[i][j]);
					}
				}
			}
		}
	}
	void printState(State state){
		for(int i=0;i<5;i++){
			for(int j=0;j<5;j++){
				System.out.print(state.config[i][j]+" ");
			}
			System.out.println();
		}
	}
	
	public static State getState(State state){
		for(int i=0;i<5;i++){
			for(int j=0;j<5;j++){
				state.config[i][j] = button[i][j].color;
			}
		}
		
		return state;
	}
	
	public static int goalTest(State state){
		int counter = 0;
		for(int i=0;i<5;i++){
			for(int j=0;j<5;j++){
				if(state.config[i][j]==0){
					counter = 1;
				}
			}
		}
		if(counter==0){
		JFrame goalFrame = new JFrame();
		goalFrame.setSize(75,100); //creates the jframe
		goalFrame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		goalFrame.setVisible(true);
		
		JPanel goalPanel = new JPanel();
		JLabel text = new JLabel();
		text.setText("You Win!");
		goalPanel.add(text);
		goalFrame.add(goalPanel);
		}
		
		return counter;
	}


	
	private void Result(State currentState, int x, int y){
		State state = new State();
		state = currentState;
		for(int i=0;i<5;i++){
			for(int j=0;j<5;j++){
				if((i==x&&j==y)||(i==x&&j==y+1)||(i==x&&j==y-1)||(i==x+1&&j==y)||(i==x-1&&j==y)){
					if(state.config[i][j]==1){
						state.config[i][j]=0;
					}
					else if(state.config[i][j]==0){
						state.config[i][j]=1;
					}
				}		
			}
		}
		state.actionx.add(x);
		state.actiony.add(y);
		frontier.addLast(state);
	}
	
	private void createSolutionFrame(State currentState){
		JFrame sFrame = new JFrame();
		sFrame.setSize(500,500); //creates the jframe
		sFrame.setTitle("Solution");
		sFrame.setVisible(true);
		GridLayout g = new GridLayout(5,5);
		sFrame.setLayout(g);
		JButton [][] sButton = new JButton[5][5];
		for(int i=0;i<5;i++){
			for(int j=0;j<5;j++){
				int x = currentState.actionx.getFirst();
				int y = currentState.actiony.getFirst();
				sButton[i][j] = new JButton();
				if(i==x&&j==y){
					sButton[i][j].setBackground(Color.BLACK);
				}
				else{
					sButton[i][j].setBackground(Color.WHITE);
				}
				sFrame.add(sButton[i][j]);
			}
		}
	}
	
	private void treeSearch(){
		int counter;
		State currentState = null;
		frontier = new LinkedList <State>();
		State initialState = new State();
		initialState = getState(initialState);
		initialState.id = 1;
		frontier.add(initialState);
		while(frontier.isEmpty()==false){
			currentState = new State();
			counter=goalTest(currentState);
			
			if(counter==1){
				for(int i=0;i<5;i++){
					for(int j=0;j<5;j++){
						Result(currentState, i, j);
					}
				}
			}
			else if(counter==0){
				currentState = frontier.getFirst();
				break;
			}
		}
		createSolutionFrame(currentState);
	
	}
	
	
	public static void openFile(){
		JFileChooser fc = new JFileChooser(); //instantiante a file chooser
		int result = fc.showOpenDialog(null); //opens a dialog
		
		if(result==JFileChooser.APPROVE_OPTION){ //once the file is selected and clicked ok
			File file = fc.getSelectedFile(); //get the file
		
			String filename=file.toString(); //get the file name
			try{
				x = new Scanner(new File(filename)); //opens the file for reading
			}
			catch(Exception e1){
				System.out.println("File could not be found"); //if file is not found
			}
			
		}
	}
	
	public static void readFile(){
				
	//	String [][] a = new String[5][5]; //stores the configuration in an array
		for(int i=0;i<5;i++){ //as long as there is still something to read, store it in the array
			for(int j=0;j<5;j++){
				if(loadCounter==0){
				a[i][j] = x.next(); 
				}
				if(Integer.parseInt(a[i][j])==1){
					button[i][j].setBackground(Color.BLACK);
					button[i][j].color = 1;
				}
				else if(Integer.parseInt(a[i][j])==0){
					button[i][j].setBackground(Color.WHITE);
					button[i][j].color = 0;
				}
				
			}
		}	
	}
	
	public static void closeFile(){
		x.close();
	}

	@Override
	public void actionPerformed(ActionEvent e) {
		// TODO Auto-generated method stub
		if(e.getSource()==rand){
			for(int i=0;i<5;i++){
				for(int j=0;j<5;j++){
					int count = (int) (Math.random()*(1-0+1)) + 0; // *(high-low+1) will result in 1 or 0
					button[i][j].color = count;
					setConfiguration(button[i][j]);
				}
			}
		
		}
		else if(e.getSource()==solve){
			treeSearch();
		}
		
		else if(e.getSource()==load){
			openFile(); 
			readFile(); 
			closeFile();
			loadCounter = loadCounter+1;
			//loadCounter will be used for reset, if it is equal to 1, it would reset to the
			//file's configuration
		}
		
		if(e.getSource()==reset){
			if(loadCounter==1){ //resets accdg to the file's configuration
				for(int i=0;i<5;i++){
					for(int j=0;j<5;j++){
						if(Integer.parseInt(a[i][j])==1){
							button[i][j].setBackground(Color.BLACK);
						}
						else if(Integer.parseInt(a[i][j])==0){
							button[i][j].setBackground(Color.WHITE);
						}
					}
				}
			}
			else if(loadCounter==0){ //resets it randomly
				for(int i=0;i<5;i++){
					for(int j=0;j<5;j++){
						button[i][j].x = i;
						button[i][j].y = j;
						int count = (int) (Math.random()*(1-0+1)) + 0; // *(high-low+1) will result in 1 or 0
						button[i][j].color = count;
						setConfiguration(button[i][j]);
					}
				}
			}
		}
		
		
	}
}


