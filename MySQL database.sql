CREATE DATABASE InlämningDatabaser;
USE inlämningDatabaser;

DROP TABLE IF EXISTS Golfer;
DROP TABLE IF EXISTS Golfclub;
DROP TABLE IF EXISTS Employee;
DROP TABLE IF EXISTS Golfcourse;
DROP TABLE IF EXISTS Golfer_Has_Golfclub;

DROP TABLE IF EXISTS Golfer, Golfclub, Employee, Golfcourse, Golfer_Has_Golfclub CASCADE;

SELECT * FROM Golfer;
SELECT * FROM Golfclub;
SELECT * FROM Employee;
SELECT * FROM Golfcourse;
SELECT * FROM Golfer_has_Golfclub;

CREATE TABLE Golfer(
Golfer_ID INT PRIMARY KEY AUTO_INCREMENT,
Golfer_Name VARCHAR(45),
Golfer_Email VARCHAR(320),
Golfer_Handicap INT
);

CREATE TABLE Golfclub(
Golfclub_ID INT PRIMARY KEY AUTO_INCREMENT,
Golfclub_Name VARCHAR(45),
Golfclub_Country VARCHAR(45),
Golfclub_City VARCHAR(45)
);

CREATE TABLE Employee(
Employee_ID INT PRIMARY KEY AUTO_INCREMENT,
Employee_Name VARCHAR(45),
Employee_Email VARCHAR(45),
Employee_Salary INT,
Golfclub_ID INT,
FOREIGN KEY(Golfclub_ID) REFERENCES Golfclub(Golfclub_ID)
);

CREATE TABLE Golfcourse(
Golfcourse_ID INT PRIMARY KEY AUTO_INCREMENT,
Golfcourse_Name VARCHAR(45),
Golfcourse_Holes INT,
Golfcourse_Greenfee INT,
Golfclub_ID INT,
FOREIGN KEY(golfclub_ID) REFERENCES Golfclub(Golfclub_ID)
);

CREATE TABLE Golfer_Has_Golfclub(
Golfclub_ID INT,
Golfer_ID INT,
FOREIGN KEY(Golfclub_ID) REFERENCES Golfclub(Golfclub_ID),
FOREIGN KEY(Golfer_ID) REFERENCES Golfer(Golfer_ID)
);

DROP PROCEDURE populate_golfer;
DROP PROCEDURE populate_golfclub;
DROP PROCEDURE populate_Golfcourse;
DROP PROCEDURE populate_Employee;
DROP PROCEDURE populate_golfer_has_golfclub;

CALL populate_golfer;
CALL populate_golfclub;
CALL populate_Golfcourse;
CALL populate_Employee;
CALL populate_Golfer_has_golfclub;

DELIMITER $$
CREATE PROCEDURE populate_Golfer()
BEGIN
	INSERT INTO Golfer VALUES (1, "Joel", "Joel@mail.com", 15);
	INSERT INTO Golfer VALUES (2, "Tim", "Tim@mail.com", 13);
	INSERT INTO Golfer VALUES (3, "Koffe", "Koffe@mail.com", 7);
	INSERT INTO Golfer VALUES (4, "Jonas", "Jonas@mail.com", 25);
	INSERT INTO Golfer VALUES (5, "Robert", "Robert@mail.com", 23);
	INSERT INTO Golfer VALUES (6, "Marcus", "Marcus@mail.com", 11);
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE populate_Golfclub()
BEGIN
	INSERT INTO Golfclub VALUES (1, "Sankt Jörgen", "Sweden", "Göteborg");
	INSERT INTO Golfclub VALUES (2, "Albatross", "Sweden", "Göteborg");
	INSERT INTO Golfclub VALUES (3, "Trysilfjellet Golf", "Norway", "Hedmark");
	INSERT INTO Golfclub VALUES (4, "Nordvestjysk Golfklub", "Denmark", "North Jutland");
	INSERT INTO Golfclub VALUES (5, "Kytäjä", "Finland", "Hyvinkää");
	INSERT INTO Golfclub VALUES (6, "Trump International Doral", "USA", "Miami");
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE populate_Golfcourse()
BEGIN
	INSERT INTO Golfcourse VALUES (1, "Sankt Jörgen 18 hålsbana", 18, 750, 1);
	INSERT INTO Golfcourse VALUES (2, "Eagle Korten", 9, 450, 2);
	INSERT INTO Golfcourse VALUES (3, "Alba 18-hål", 18, 800, 2);
	INSERT INTO Golfcourse VALUES (4, "Lutefisk Links", 18, 1100, 3);
	INSERT INTO Golfcourse VALUES (5, "Smørrebrød Swings", 18, 1000, 4);
	INSERT INTO Golfcourse VALUES (6, "kulkuluukku", 18, 200, 5);
	INSERT INTO Golfcourse VALUES (7, "Tiger Course", 18, 3000, 6);
	INSERT INTO Golfcourse VALUES (8, "Golden Palm", 18, 5000, 6);
	INSERT INTO Golfcourse VALUES (9, "Silver Fox", 18, 3700, 6);
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE populate_Employee()
BEGIN
    INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) VALUES ('Göran', 'Göran@mail.com', 20000, 1);
    INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) VALUES ('Sven', 'Sven@mail.com', 30000, 1);
    INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) VALUES ('Stefan', 'Stefan@mail.com', 50000, 2);
    INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) VALUES ('Ben', 'Ben@mail.com', 1500, 3);
    INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) VALUES ('Karl', 'Karl@mail.com', 2304, 2);
    INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) VALUES ('Oskar', 'Oskar@mail.com', 7000, 3);
    INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) VALUES ('Charles', 'Charles@mail.com', 2435, 4);
    INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) VALUES ('Stellan', 'Stellan@mail.com', 7537, 5);
    INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) VALUES ('Gösta', 'Gösta@mail.com', 73453, 6);
    INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) VALUES ('Tobias', 'Tobias@mail.com', 73452, 5);
    INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) VALUES ('Mathias', 'Mathias@mail.com', 12435, 4);
    INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) VALUES ('Daniel', 'Daniel@mail.com', 67234, 6);
    INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) VALUES ('Markus', 'Markus@mail.com', 84323, 6);
END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE populate_Golfer_has_golfclub()
BEGIN
INSERT INTO Golfer_Has_Golfclub (Golfer_ID, Golfclub_ID) VALUES (1, 1);
INSERT INTO Golfer_Has_Golfclub (Golfer_ID, Golfclub_ID) VALUES (2, 2);
INSERT INTO Golfer_Has_Golfclub (Golfer_ID, Golfclub_ID) VALUES (3, 5);
INSERT INTO Golfer_Has_Golfclub (Golfer_ID, Golfclub_ID) VALUES (4, 3);
INSERT INTO Golfer_Has_Golfclub (Golfer_ID, Golfclub_ID) VALUES (5, 6);
INSERT INTO Golfer_Has_Golfclub (Golfer_ID, Golfclub_ID) VALUES (6, 4);
END$$
DELIMITER ;

DROP VIEW golfer_golfclub_view;
DROP VIEW Golfclub_has_Courses;
    
SELECT * FROM golfer_golfclub_view;
SELECT * FROM Golfclub_has_courses;
    
CREATE VIEW Golfer_Golfclub_View AS
SELECT
    GHG.Golfer_ID,
    G.Golfer_Name,
    G.Golfer_Email,
    G.Golfer_Handicap,
    GHG.Golfclub_ID,
    GC.Golfclub_Name,
    GC.Golfclub_Country,
    GC.Golfclub_City
FROM
    Golfer_Has_Golfclub GHG
JOIN
    Golfer G ON GHG.Golfer_ID = G.Golfer_ID
JOIN
    Golfclub GC ON GHG.Golfclub_ID = GC.Golfclub_ID;
    
    CREATE VIEW Golfclub_has_Courses AS
    SELECT
    GC.Golfclub_ID,
    GC.Golfclub_Name,
    GC.Golfclub_Country,
    GC.Golfclub_City,
    GCourse.Golfcourse_ID,
    GCourse.Golfcourse_Name,
    GCourse.Golfcourse_Holes,
    GCourse.Golfcourse_Greenfee
FROM
    Golfclub GC
JOIN
    Golfcourse GCourse ON GC.Golfclub_ID = GCourse.Golfclub_ID;

    