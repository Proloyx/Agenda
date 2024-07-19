CREATE DATABASE WorkSchedule;

-- Table Users
CREATE TABLE Users (
    UserID SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL  -- User password
);

-- Table WorkCenters
CREATE TABLE WorkCenters (
    CenterID SERIAL PRIMARY KEY,
    UserID INT not null REFERENCES Users(UserID) ON DELETE CASCADE,
    Name VARCHAR(100) NOT NULL,
    Address VARCHAR(200),
    GrossRate DECIMAL(10, 2) NOT NULL,  -- Gross rate per hour
    PaymentDay int not null,
    NetRate DECIMAL(10, 2) NOT NULL  -- Net rate per hour
);

-- Table Schedules
CREATE TABLE Schedules (
    ScheduleID SERIAL PRIMARY KEY,
    UserID INT not null REFERENCES Users(UserID),
    CenterID INT not null REFERENCES WorkCenters(CenterID),
    WorkDate DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
	WorkedHours int not null,
	Description varchar (250),
    CONSTRAINT unique_schedule UNIQUE (UserID, WorkDate, StartTime),
    CONSTRAINT chk_times CHECK (EndTime > StartTime)  -- Ensure that end time is after start time
);
