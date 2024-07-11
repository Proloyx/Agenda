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
    UserID INT REFERENCES Users(UserID) ON DELETE CASCADE,
    Name VARCHAR(100) NOT NULL,
    Address VARCHAR(200),
    GrossRate DECIMAL(10, 2) NOT NULL,  -- Gross rate per hour
    NetRate DECIMAL(10, 2) NOT NULL  -- Net rate per hour
);

-- Table Schedules
CREATE TABLE Schedules (
    ScheduleID SERIAL PRIMARY KEY,
    UserID INT REFERENCES Users(UserID),
    CenterID INT REFERENCES WorkCenters(CenterID),
    WorkDate DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    CONSTRAINT unique_schedule UNIQUE (UserID, WorkDate, StartTime),
    CONSTRAINT chk_times CHECK (EndTime > StartTime)  -- Ensure that end time is after start time
);

-- Table ScheduleDetails
CREATE TABLE ScheduleDetails (
    ScheduleDetailID SERIAL PRIMARY KEY,
    ScheduleID INT REFERENCES Schedules(ScheduleID) ON DELETE CASCADE,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    CONSTRAINT chk_detail_times CHECK (EndTime > StartTime)  -- Ensure that end time is after start time
);

-- Table PaymentDates
CREATE TABLE PaymentDates (
    PaymentDateID SERIAL PRIMARY KEY,
    CenterID INT REFERENCES WorkCenters(CenterID) ON DELETE CASCADE,
    PaymentDate DATE NOT NULL
);
