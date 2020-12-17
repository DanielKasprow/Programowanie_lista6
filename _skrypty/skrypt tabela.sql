USE [Lista6]
GO

/****** Object:  Table [dbo].[Base]    Script Date: 13.12.2020 15:52:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Base](
	[Imie] [varchar](255) NULL,
	[Nazwisko] [varchar](255) NULL,
	[Ulica] [varchar](255) NULL,
	[Nr] [int] NULL,
	[Miasto] [varchar](255) NULL,
	[Kraj] [varchar](255) NULL,
	[Wiek] [int] NULL,
	[Pesel] [bigint] NULL,
	[Image] [varbinary](max) NULL,
	[varbyte] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

