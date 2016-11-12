
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/12/2016 20:55:02
-- Generated from EDMX file: F:\University\Programming\GitHubLocal\SocialNetwork\SocialNetwork\SocialNetwork.DAL\EF\SocialNetwork.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SocialNetworkDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Profiles'
CREATE TABLE [dbo].[Profiles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Sername] nvarchar(max)  NOT NULL,
    [IdentityName] nvarchar(max)  NOT NULL,
    [CustomInfo] nvarchar(max)  NOT NULL,
    [DateOfBirth] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [PublishDate] datetime  NOT NULL,
    [ProfileId] int  NOT NULL
);
GO

-- Creating table 'Hashtags'
CREATE TABLE [dbo].[Hashtags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [PostId] int  NOT NULL,
    [ProfileId] int  NOT NULL
);
GO

-- Creating table 'FollowerSubscriber'
CREATE TABLE [dbo].[FollowerSubscriber] (
    [SubscribedOn_Id] int  NOT NULL,
    [Followers_Id] int  NOT NULL
);
GO

-- Creating table 'PostHashtag'
CREATE TABLE [dbo].[PostHashtag] (
    [Posts_Id] int  NOT NULL,
    [Hashtags_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [PK_Profiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Hashtags'
ALTER TABLE [dbo].[Hashtags]
ADD CONSTRAINT [PK_Hashtags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [SubscribedOn_Id], [Followers_Id] in table 'FollowerSubscriber'
ALTER TABLE [dbo].[FollowerSubscriber]
ADD CONSTRAINT [PK_FollowerSubscriber]
    PRIMARY KEY CLUSTERED ([SubscribedOn_Id], [Followers_Id] ASC);
GO

-- Creating primary key on [Posts_Id], [Hashtags_Id] in table 'PostHashtag'
ALTER TABLE [dbo].[PostHashtag]
ADD CONSTRAINT [PK_PostHashtag]
    PRIMARY KEY CLUSTERED ([Posts_Id], [Hashtags_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SubscribedOn_Id] in table 'FollowerSubscriber'
ALTER TABLE [dbo].[FollowerSubscriber]
ADD CONSTRAINT [FK_FollowerSubscriber_Profile]
    FOREIGN KEY ([SubscribedOn_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Followers_Id] in table 'FollowerSubscriber'
ALTER TABLE [dbo].[FollowerSubscriber]
ADD CONSTRAINT [FK_FollowerSubscriber_Profile1]
    FOREIGN KEY ([Followers_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FollowerSubscriber_Profile1'
CREATE INDEX [IX_FK_FollowerSubscriber_Profile1]
ON [dbo].[FollowerSubscriber]
    ([Followers_Id]);
GO

-- Creating foreign key on [Posts_Id] in table 'PostHashtag'
ALTER TABLE [dbo].[PostHashtag]
ADD CONSTRAINT [FK_PostHashtag_Post]
    FOREIGN KEY ([Posts_Id])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Hashtags_Id] in table 'PostHashtag'
ALTER TABLE [dbo].[PostHashtag]
ADD CONSTRAINT [FK_PostHashtag_Hashtag]
    FOREIGN KEY ([Hashtags_Id])
    REFERENCES [dbo].[Hashtags]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostHashtag_Hashtag'
CREATE INDEX [IX_FK_PostHashtag_Hashtag]
ON [dbo].[PostHashtag]
    ([Hashtags_Id]);
GO

-- Creating foreign key on [PostId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_PostComment]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostComment'
CREATE INDEX [IX_FK_PostComment]
ON [dbo].[Comments]
    ([PostId]);
GO

-- Creating foreign key on [ProfileId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_ProfileComment]
    FOREIGN KEY ([ProfileId])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileComment'
CREATE INDEX [IX_FK_ProfileComment]
ON [dbo].[Comments]
    ([ProfileId]);
GO

-- Creating foreign key on [ProfileId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_ProfilePost]
    FOREIGN KEY ([ProfileId])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfilePost'
CREATE INDEX [IX_FK_ProfilePost]
ON [dbo].[Posts]
    ([ProfileId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------