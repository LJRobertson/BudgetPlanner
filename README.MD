# Budget Builder

Budget Builder is a final project for Eleven Fifty Academy. The goal was to make a n-tier MVC application using at least three custom tables and connections within a database. Budget Builder uses a category, transaction, memo, budget, and junction table to allow users to keep track of their finances.

## Installation
To install, please clone or download the repository from [GitHub]( https://github.com/LJRobertson/BudgetPlanner).


## Usage

Open the repository in Visual Studio Community. 

Set BudgetBuilder.WebMVC as the startup project and run the program. Upon running the application, your internet browser will launch and bring you to the Budget Builder landing page. From here, click the Register button in the upper right to get started.

### Registration

An email address and password will be needed to register.

### Budget
It all starts here. Select create to set up a new budget with a name and budget amount.


#### Create a Category

Categories are sections of your budget. Enter a name such as utilities, auto, grocery, etc.

#### Category Limit

Now that you have a budget and a category or two, add some limits. From the Category Limit index, select create, then choose the budget the category is for, the category, and set a limit. You can always change this limit later using edit.

#### Transaction

Enter a transaction for your budget to start coming together. Enter the merchant name, amount, date, budget, and the category the transaction is for. Don’t worry if the budget isn’t already assigned to the category, the program will add it for you.

#### Memo

To remind yourself of what you purchased, add a memo. From the memo index, click create. Choose the transaction the memo is for, enter the memo and save.

#### Budget Detail

This is where the program comes together. After entering transactions, go to Budgets and click Detail to view your budget. Transactions will be sorted to the correct category and then by date. Categories over budget will show the available in red. Under budget will show in green.


## Contributing
Authored by Lindsey Robertson.
