CREATE DATABASE [SqlClientIssue415];
GO

USE [SqlClientIssue415];
GO

/****** Object:  Table [dbo].[TestTable]    Script Date: 2/15/2020 7:02:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TestTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Geography] [geography] NULL,
	[Geometry] [geometry] NULL,
	[HierarchyId] [hierarchyid] NULL,
 CONSTRAINT [PK_TestTabe] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
