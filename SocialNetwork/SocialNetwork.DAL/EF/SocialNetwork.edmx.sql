
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/13/2016 14:17:57
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

IF OBJECT_ID(N'[dbo].[FK_FollowerSubscriber_Profile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FollowerSubscriber] DROP CONSTRAINT [FK_FollowerSubscriber_Profile];
GO
IF OBJECT_ID(N'[dbo].[FK_FollowerSubscriber_Profile1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FollowerSubscriber] DROP CONSTRAINT [FK_FollowerSubscriber_Profile1];
GO
IF OBJECT_ID(N'[dbo].[FK_PostHashtag_Post]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostHashtag] DROP CONSTRAINT [FK_PostHashtag_Post];
GO
IF OBJECT_ID(N'[dbo].[FK_PostHashtag_Hashtag]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostHashtag] DROP CONSTRAINT [FK_PostHashtag_Hashtag];
GO
IF OBJECT_ID(N'[dbo].[FK_PostComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_PostComment];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_ProfileComment];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfilePost]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_ProfilePost];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Profiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Profiles];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[Hashtags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Hashtags];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[FollowerSubscriber]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FollowerSubscriber];
GO
IF OBJECT_ID(N'[dbo].[PostHashtag]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostHashtag];
GO

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
    [DateOfBirth] datetime  NOT NULL
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

-- Creating table 'ReposterPost'
CREATE TABLE [dbo].[ReposterPost] (
    [Reposters_Id] int  NOT NULL,
    [RepostedPosts_Id] int  NOT NULL
);
GO

-- Creating table 'LikerPost'
CREATE TABLE [dbo].[LikerPost] (
    [LikeVoices_Id] int  NOT NULL,
    [LikedPosts_Id] int  NOT NULL
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

-- Creating primary key on [Reposters_Id], [RepostedPosts_Id] in table 'ReposterPost'
ALTER TABLE [dbo].[ReposterPost]
ADD CONSTRAINT [PK_ReposterPost]
    PRIMARY KEY CLUSTERED ([Reposters_Id], [RepostedPosts_Id] ASC);
GO

-- Creating primary key on [LikeVoices_Id], [LikedPosts_Id] in table 'LikerPost'
ALTER TABLE [dbo].[LikerPost]
ADD CONSTRAINT [PK_LikerPost]
    PRIMARY KEY CLUSTERED ([LikeVoices_Id], [LikedPosts_Id] ASC);
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

-- Creating foreign key on [Reposters_Id] in table 'ReposterPost'
ALTER TABLE [dbo].[ReposterPost]
ADD CONSTRAINT [FK_ReposterPost_Profile]
    FOREIGN KEY ([Reposters_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RepostedPosts_Id] in table 'ReposterPost'
ALTER TABLE [dbo].[ReposterPost]
ADD CONSTRAINT [FK_ReposterPost_Post]
    FOREIGN KEY ([RepostedPosts_Id])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReposterPost_Post'
CREATE INDEX [IX_FK_ReposterPost_Post]
ON [dbo].[ReposterPost]
    ([RepostedPosts_Id]);
GO

-- Creating foreign key on [LikeVoices_Id] in table 'LikerPost'
ALTER TABLE [dbo].[LikerPost]
ADD CONSTRAINT [FK_LikerPost_Profile]
    FOREIGN KEY ([LikeVoices_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [LikedPosts_Id] in table 'LikerPost'
ALTER TABLE [dbo].[LikerPost]
ADD CONSTRAINT [FK_LikerPost_Post]
    FOREIGN KEY ([LikedPosts_Id])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LikerPost_Post'
CREATE INDEX [IX_FK_LikerPost_Post]
ON [dbo].[LikerPost]
    ([LikedPosts_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------