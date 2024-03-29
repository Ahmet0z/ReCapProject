use ReCapProject

CREATE TABLE [dbo].[Brands] (
    [BrandId]   INT          IDENTITY (1, 1) NOT NULL,
    [BrandName] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([BrandId] ASC)
);

CREATE TABLE [dbo].[Colors] (
    [ColorId]   INT          IDENTITY (1, 1) NOT NULL,
    [ColorName] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ColorId] ASC)
);


CREATE TABLE [dbo].[Cars] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [ColorId]     INT          NOT NULL,
    [BrandId]     INT          NOT NULL,
    [CarName]     VARCHAR (50) NOT NULL,
    [ModelYear]   INT          NOT NULL,
    [DailyPrice]  DECIMAL (18) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    [Findeks]     INT          NOT NULL,
	[Plate]		  VARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ColorId]) REFERENCES [dbo].[Colors] ([ColorId]),
    FOREIGN KEY ([BrandId]) REFERENCES [dbo].[Brands] ([BrandId])
);


CREATE TABLE [dbo].[Users] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]    VARCHAR (50)    NOT NULL,
    [LastName]     VARCHAR (50)    NOT NULL,
    [Email]        VARCHAR (50)    NOT NULL,
    [PasswordSalt] VARBINARY (500) NOT NULL,
    [PasswordHash] VARBINARY (500) NOT NULL,
    [Status]       BIT             NOT NULL,
    [Findeks]      INT             NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[OperationClaims] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (500) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[UserOperationClaims] (
    [Id]               INT IDENTITY (1, 1) NOT NULL,
    [UserId]           INT NOT NULL,
    [OperationClaimId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[CarImages] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [CarId]     INT           NOT NULL,
    [ImagePath] VARCHAR (MAX) NOT NULL,
    [Date]      DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CarId]) REFERENCES [dbo].[Cars] ([Id])
);

CREATE TABLE [dbo].[Payments] (
    [PaymentId]        INT           IDENTITY (1, 1) NOT NULL,
    [UserId]       INT           NOT NULL,
    [CreditCardNumber] VARCHAR (16)  NOT NULL,
    [Price]            VARCHAR (500) NOT NULL,
    [ExpirationDate]   VARCHAR (4)   NOT NULL,
    [SecurityCode]     VARCHAR (3)   NOT NULL,
    PRIMARY KEY CLUSTERED ([PaymentId] ASC),
	FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[Rentals] (
    [Id]         INT  IDENTITY (1, 1) NOT NULL,
    [CarId]      INT  NOT NULL,
    [UserId]     INT  NOT NULL,
    [RentDate]   DATE NOT NULL,
    [ReturnDate] DATE NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CarId]) REFERENCES [dbo].[Cars] ([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[Cards] (
    [CardId]           INT          IDENTITY (1, 1) NOT NULL,
    [UserId]           INT          NOT NULL,
    [OwnerName]        VARCHAR (50) NOT NULL,
    [CreditCardNumber] VARCHAR (16) NOT NULL,
    [ExpirationDate]   VARCHAR (4)  NOT NULL,
    [SecurityCode]     VARCHAR (3)  NOT NULL,
    [Debts]            DECIMAL (18) NULL,
    PRIMARY KEY CLUSTERED ([CardId] ASC)
);

