USE [master]
GO
/****** Object:  Database [ReCapProject]    Script Date: 10.04.2021 18:04:54 ******/
CREATE DATABASE [ReCapProject]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ReCapProject', FILENAME = N'C:\Users\kaygi\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\ReCapProject.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ReCapProject_log', FILENAME = N'C:\Users\kaygi\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\ReCapProject.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ReCapProject] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ReCapProject].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ReCapProject] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [ReCapProject] SET ANSI_NULLS ON 
GO
ALTER DATABASE [ReCapProject] SET ANSI_PADDING ON 
GO
ALTER DATABASE [ReCapProject] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [ReCapProject] SET ARITHABORT ON 
GO
ALTER DATABASE [ReCapProject] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ReCapProject] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ReCapProject] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ReCapProject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ReCapProject] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [ReCapProject] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [ReCapProject] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ReCapProject] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [ReCapProject] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ReCapProject] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ReCapProject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ReCapProject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ReCapProject] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ReCapProject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ReCapProject] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ReCapProject] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ReCapProject] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ReCapProject] SET RECOVERY FULL 
GO
ALTER DATABASE [ReCapProject] SET  MULTI_USER 
GO
ALTER DATABASE [ReCapProject] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ReCapProject] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ReCapProject] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ReCapProject] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ReCapProject] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ReCapProject] SET QUERY_STORE = OFF
GO
USE [ReCapProject]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [ReCapProject]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 10.04.2021 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarImages]    Script Date: 10.04.2021 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CarId] [int] NOT NULL,
	[ImagePath] [nvarchar](max) NULL,
	[Date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 10.04.2021 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BrandId] [int] NOT NULL,
	[ColorId] [int] NOT NULL,
	[ModelYear] [int] NOT NULL,
	[DailyPrice] [decimal](18, 0) NOT NULL,
	[Description] [nvarchar](1500) NULL,
	[FindeksPoint] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colors]    Script Date: 10.04.2021 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ColorName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 10.04.2021 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[FindeksPoint] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperationClaims]    Script Date: 10.04.2021 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 10.04.2021 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Month] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[CVV] [int] NOT NULL,
	[CardNumber] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rentals]    Script Date: 10.04.2021 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rentals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CarId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[RentDate] [datetime] NULL,
	[ReturnDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserOperationClaims]    Script Date: 10.04.2021 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOperationClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[OperationClaimId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10.04.2021 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[PasswordSalt] [varbinary](500) NOT NULL,
	[PasswordHash] [varbinary](500) NOT NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([Id], [BrandName]) VALUES (5, N'Opel')
INSERT [dbo].[Brands] ([Id], [BrandName]) VALUES (6, N'Dacia')
INSERT [dbo].[Brands] ([Id], [BrandName]) VALUES (7, N'Tofaş')
INSERT [dbo].[Brands] ([Id], [BrandName]) VALUES (8, N'Lamborghini')
INSERT [dbo].[Brands] ([Id], [BrandName]) VALUES (1004, N'Porsche')
INSERT [dbo].[Brands] ([Id], [BrandName]) VALUES (2004, N'Ferrari')
INSERT [dbo].[Brands] ([Id], [BrandName]) VALUES (3004, N'Hyundai')
SET IDENTITY_INSERT [dbo].[Brands] OFF
GO
SET IDENTITY_INSERT [dbo].[CarImages] ON 

INSERT [dbo].[CarImages] ([Id], [CarId], [ImagePath], [Date]) VALUES (37027, 23004, N'/images/ba8ae516-6744-46e4-b642-cd8d8d97802d926_17_29.jpg', CAST(N'2021-04-10T17:29:58.927' AS DateTime))
INSERT [dbo].[CarImages] ([Id], [CarId], [ImagePath], [Date]) VALUES (37028, 23004, N'/images/f8a14d7f-ed8c-4a6d-9381-c9c387de0964811_17_30.jpg', CAST(N'2021-04-10T17:30:20.813' AS DateTime))
INSERT [dbo].[CarImages] ([Id], [CarId], [ImagePath], [Date]) VALUES (37029, 23004, N'/images/d7a9c742-14fb-4f28-8ff8-9467995ebb0b178_17_30.jpg', CAST(N'2021-04-10T17:30:29.180' AS DateTime))
INSERT [dbo].[CarImages] ([Id], [CarId], [ImagePath], [Date]) VALUES (37030, 23004, N'/images/d4d50fba-d5ab-4cd3-beb0-de7a2c275361447_17_30.jpg', CAST(N'2021-04-10T17:30:58.447' AS DateTime))
INSERT [dbo].[CarImages] ([Id], [CarId], [ImagePath], [Date]) VALUES (37031, 23004, N'/images/3cbdd45c-3ddc-4fd4-85a7-1cef38c078d5983_17_31.jpg', CAST(N'2021-04-10T17:31:04.983' AS DateTime))
INSERT [dbo].[CarImages] ([Id], [CarId], [ImagePath], [Date]) VALUES (37032, 23005, N'/images/195fcdca-77d5-480f-8bfc-52a44db1b014920_17_38.jpg', CAST(N'2021-04-10T17:38:13.920' AS DateTime))
INSERT [dbo].[CarImages] ([Id], [CarId], [ImagePath], [Date]) VALUES (37033, 23005, N'/images/38f5ceea-e062-4c21-8415-50453a822116365_17_38.jpg', CAST(N'2021-04-10T17:38:24.367' AS DateTime))
INSERT [dbo].[CarImages] ([Id], [CarId], [ImagePath], [Date]) VALUES (37034, 23005, N'/images/5f95bd66-c0d0-4684-8749-02531a01d034807_17_38.jpg', CAST(N'2021-04-10T17:38:32.807' AS DateTime))
INSERT [dbo].[CarImages] ([Id], [CarId], [ImagePath], [Date]) VALUES (37035, 23005, N'/images/44f8864d-2059-40f6-8360-d71f6d744041156_17_38.jpg', CAST(N'2021-04-10T17:38:38.157' AS DateTime))
INSERT [dbo].[CarImages] ([Id], [CarId], [ImagePath], [Date]) VALUES (37036, 23005, N'/images/37ae3e0f-564c-437c-b3d0-81ec1fe84300812_17_38.jpg', CAST(N'2021-04-10T17:38:43.813' AS DateTime))
SET IDENTITY_INSERT [dbo].[CarImages] OFF
GO
SET IDENTITY_INSERT [dbo].[Cars] ON 

INSERT [dbo].[Cars] ([Id], [BrandId], [ColorId], [ModelYear], [DailyPrice], [Description], [FindeksPoint]) VALUES (23004, 8, 6, 2021, CAST(5000 AS Decimal(18, 0)), N'Son Model Lamborghini 1 Aylığına Kiralık', CAST(1250 AS Decimal(18, 0)))
INSERT [dbo].[Cars] ([Id], [BrandId], [ColorId], [ModelYear], [DailyPrice], [Description], [FindeksPoint]) VALUES (23005, 6, 7, 2016, CAST(100 AS Decimal(18, 0)), N'Beyaz Duster Temiz Kullanılmış', CAST(25 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Cars] OFF
GO
SET IDENTITY_INSERT [dbo].[Colors] ON 

INSERT [dbo].[Colors] ([Id], [ColorName]) VALUES (4, N'Kırmızı')
INSERT [dbo].[Colors] ([Id], [ColorName]) VALUES (5, N'Mavi')
INSERT [dbo].[Colors] ([Id], [ColorName]) VALUES (6, N'Siyah')
INSERT [dbo].[Colors] ([Id], [ColorName]) VALUES (7, N'Beyaz')
INSERT [dbo].[Colors] ([Id], [ColorName]) VALUES (8, N'Yeşil')
INSERT [dbo].[Colors] ([Id], [ColorName]) VALUES (2003, N'Sarı')
INSERT [dbo].[Colors] ([Id], [ColorName]) VALUES (5003, N'Turuncu')
SET IDENTITY_INSERT [dbo].[Colors] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [UserId], [CompanyName], [FindeksPoint]) VALUES (10002, 15002, N'Admin CompanyName', CAST(304 AS Decimal(18, 0)))
INSERT [dbo].[Customers] ([Id], [UserId], [CompanyName], [FindeksPoint]) VALUES (10003, 15003, N'CompanyName', CAST(251 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[OperationClaims] ON 

INSERT [dbo].[OperationClaims] ([Id], [Name]) VALUES (1, N'Admin')
SET IDENTITY_INSERT [dbo].[OperationClaims] OFF
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([Id], [CustomerId], [FirstName], [LastName], [Month], [Year], [CVV], [CardNumber]) VALUES (2003, 10003, N'User FirstName', N'User LastName', 12, 2027, 123, N'1234123412341234')
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
SET IDENTITY_INSERT [dbo].[Rentals] ON 

INSERT [dbo].[Rentals] ([Id], [CarId], [CustomerId], [RentDate], [ReturnDate]) VALUES (30003, 23005, 10003, CAST(N'2021-04-11T00:00:00.000' AS DateTime), CAST(N'2021-04-13T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Rentals] OFF
GO
SET IDENTITY_INSERT [dbo].[UserOperationClaims] ON 

INSERT [dbo].[UserOperationClaims] ([Id], [UserId], [OperationClaimId]) VALUES (1, 15002, 1)
SET IDENTITY_INSERT [dbo].[UserOperationClaims] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [PasswordSalt], [PasswordHash], [Status]) VALUES (15002, N'Admin FİrstName', N'Admin LastName', N'admin@admin.com', 0x42EF81858AD264CDA548BD7DBBD09EA4BD1632AC4B5382A64AC75C1085761937B186EF1D82A76CE2C3EC5B9B75CD650DEBC605FA9338EA18B265FE0CEE50B951497F7857B1F09ED0C09FF76B5DB585F582ADCAE2A525E28B7DF1F4D29645E21C97A6498C77D98C8E04CE8D67D757B0915A23AC001582959C45700F02981C6DE7, 0x619D494EC532DFC9BCF14E3DD6290472627ADFABDB48BC7B58E783DC1C865B6AA9E5F288EA47A1B337F43A9B5AF47C07B104A555800A6DB8110B1A1D14D1794F, 1)
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [PasswordSalt], [PasswordHash], [Status]) VALUES (15003, N'FirstName', N'LastName', N'user@user.com', 0xF55543EDC993A6AC8037AF8FA245A395B5120A7C9BF39D726861C7C2F76E6D49175581A66281CB7EBDAAA729BBEEC4D962AA8E8C1A130D203A4E4FBBD5C7E326797599B776E64F95EFE973D7BB4DC872DE55124B14AA4AE9ECBB5809F3132F3E80EDE2DF60B61B8F2928DD70752AC65839316AF0DEEEB2BB82110B4A80A161B6, 0x03A9D173BEC3777666405D9CBED1C6238975377D1B45C51FE2863FC31A927E86230DA47489B59A6D9A3C5287505753C3F3C03CF680FB6E0AF25B4DA5BCAAFEC4, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
USE [master]
GO
ALTER DATABASE [ReCapProject] SET  READ_WRITE 
GO
