USE [master]
GO
/****** Object:  Database [db2]    Script Date: 14.01.2025 13:19:10 ******/
CREATE DATABASE [db2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\db2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'db2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\db2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [db2] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db2] SET ARITHABORT OFF 
GO
ALTER DATABASE [db2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db2] SET  ENABLE_BROKER 
GO
ALTER DATABASE [db2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db2] SET RECOVERY FULL 
GO
ALTER DATABASE [db2] SET  MULTI_USER 
GO
ALTER DATABASE [db2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [db2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'db2', N'ON'
GO
ALTER DATABASE [db2] SET QUERY_STORE = ON
GO
ALTER DATABASE [db2] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [db2]
GO
/****** Object:  Table [dbo].[Scores]    Script Date: 14.01.2025 13:19:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scores](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Score] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users_table]    Script Date: 14.01.2025 13:19:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_table](
	[kullanici_adi] [varchar](50) NULL,
	[sifre] [varchar](50) NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [db2] SET  READ_WRITE 
GO
