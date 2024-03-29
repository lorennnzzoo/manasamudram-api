USE [master]
GO
/****** Object:  Database [HouseHoldTest]    Script Date: 2/21/2024 4:14:55 PM ******/
CREATE DATABASE [HouseHoldTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HouseHoldTest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\HouseHoldTest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HouseHoldTest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\HouseHoldTest_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [HouseHoldTest] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HouseHoldTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HouseHoldTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HouseHoldTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HouseHoldTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HouseHoldTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HouseHoldTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [HouseHoldTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HouseHoldTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HouseHoldTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HouseHoldTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HouseHoldTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HouseHoldTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HouseHoldTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HouseHoldTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HouseHoldTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HouseHoldTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HouseHoldTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HouseHoldTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HouseHoldTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HouseHoldTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HouseHoldTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HouseHoldTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HouseHoldTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HouseHoldTest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HouseHoldTest] SET  MULTI_USER 
GO
ALTER DATABASE [HouseHoldTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HouseHoldTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HouseHoldTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HouseHoldTest] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [HouseHoldTest] SET DELAYED_DURABILITY = DISABLED 
GO
USE [HouseHoldTest]
GO
/****** Object:  Table [dbo].[App_Users]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[App_Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[DriverName] [nvarchar](50) NULL,
 CONSTRAINT [PK_App_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EndScanning]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EndScanning](
	[StartScanning] [bit] NULL,
	[Endscanning] [bit] NULL,
	[DriveName] [nvarchar](max) NULL,
	[Date] [date] NULL,
	[Tripcount] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HouseHold]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HouseHold](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QRUniqueID] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[District] [nvarchar](max) NULL,
	[NumberOfPersons] [nvarchar](max) NULL,
	[DOORNo] [nvarchar](max) NULL,
 CONSTRAINT [PK_HouseHold] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MicRecording]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MicRecording](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NULL,
	[DateTimeRecorded] [datetime] NOT NULL,
	[IsRead] [bit] NOT NULL,
	[RecordedData] [varbinary](max) NULL,
	[LengthOfAudio] [int] NULL,
 CONSTRAINT [PK__MicRecor__3214EC076A4DB155] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QR]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QR](
	[UniqueID] [uniqueidentifier] NOT NULL,
	[QRData] [varbinary](max) NULL,
 CONSTRAINT [PK_QR] PRIMARY KEY CLUSTERED 
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Startsacnning]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Startsacnning](
	[Startscanning] [bit] NULL,
	[Date] [date] NULL,
	[Drivename] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TotalDryWastage]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TotalDryWastage](
	[DryWastageId] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NULL,
	[DryWasteCollected] [decimal](10, 2) NULL,
	[DryWasteReceived] [decimal](10, 2) NULL,
	[DryWasteProcessed] [decimal](10, 2) NULL,
	[DryWasteRecovery] [decimal](10, 2) NULL,
	[HousesCollected] [int] NULL,
 CONSTRAINT [PK_TotalDryWastage] PRIMARY KEY CLUSTERED 
(
	[DryWastageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TotalHHwastage]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TotalHHwastage](
	[HHWastageId] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NULL,
	[HHWasteCollected] [decimal](10, 2) NULL,
	[TotalHHHSafelyDisposed] [decimal](10, 2) NULL,
	[HousesCollected] [int] NULL,
 CONSTRAINT [PK_TotalHHwastage] PRIMARY KEY CLUSTERED 
(
	[HHWastageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TotalMixedWastage]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TotalMixedWastage](
	[MixedWastageId] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NULL,
	[MixedWasteCollected] [decimal](10, 2) NULL,
	[TotalMixedWasteDisposed] [decimal](10, 2) NULL,
	[MixedWasteRecovery] [decimal](10, 2) NULL,
	[HousesCollected] [int] NULL,
 CONSTRAINT [PK_TotalMixedWastage] PRIMARY KEY CLUSTERED 
(
	[MixedWastageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TotalWetWastage]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TotalWetWastage](
	[WetWastageId] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NULL,
	[WetWasteCollected] [decimal](10, 2) NULL,
	[WetWasteReceived] [decimal](10, 2) NULL,
	[WetWasteProcessed] [decimal](10, 2) NULL,
	[WetWasteRecovery] [decimal](10, 2) NULL,
	[HousesCollected] [int] NULL,
 CONSTRAINT [PK_TotalWastage] PRIMARY KEY CLUSTERED 
(
	[WetWastageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Trips]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trips](
	[DateandTime] [date] NULL,
	[NoOfTrips] [int] NOT NULL,
	[VehicleNumber] [nvarchar](50) NULL,
	[NoofWorkers] [int] NULL,
	[DriveName] [nvarchar](max) NULL,
	[CountTrueOrFalse] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VehiclesForMic]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehiclesForMic](
	[Id] [int] NOT NULL,
	[VehicleName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__Vehicles__3214EC07E2FBD1BE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WastageConfirmation]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WastageConfirmation](
	[WastageConfirmationId] [int] IDENTITY(1,1) NOT NULL,
	[Datetime] [datetime] NOT NULL,
	[DriverName] [nvarchar](max) NULL,
	[HouseId] [int] NULL,
	[ServiceGiven] [bit] NULL,
	[IsScannestatus] [bit] NULL,
	[Trip] [int] NULL,
 CONSTRAINT [PK_WastageConfirmation] PRIMARY KEY CLUSTERED 
(
	[WastageConfirmationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WastageInfo]    Script Date: 2/21/2024 4:14:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WastageInfo](
	[wastageid] [int] IDENTITY(1,1) NOT NULL,
	[DriverName] [nvarchar](max) NULL,
	[WetWasteCollected] [decimal](10, 2) NULL,
	[DryWasteCollected] [decimal](10, 2) NULL,
	[HHWasteCollected] [decimal](10, 2) NULL,
	[MixedWasteCollected] [decimal](10, 2) NULL,
	[DateTimeWasteLogged] [datetime] NULL,
 CONSTRAINT [PK_WastageInfo] PRIMARY KEY CLUSTERED 
(
	[wastageid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[MicRecording]  WITH CHECK ADD  CONSTRAINT [FK__MicRecord__Vehic__1ED998B2] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[VehiclesForMic] ([Id])
GO
ALTER TABLE [dbo].[MicRecording] CHECK CONSTRAINT [FK__MicRecord__Vehic__1ED998B2]
GO
USE [master]
GO
ALTER DATABASE [HouseHoldTest] SET  READ_WRITE 
GO
