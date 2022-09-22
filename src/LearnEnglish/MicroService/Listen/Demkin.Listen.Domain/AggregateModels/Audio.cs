using Demkin.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Domain.AggregateModels
{
    public class Audio : Entity<long>, IAggregateRoot
    {
        private Audio()
        { }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public int SequenceNumber { get; private set; }

        public Uri AudioUrl { get; private set; }

        public double DurationInSecond { get; private set; }

        public string Subtitles { get; private set; }

        public bool IsVisible { get; private set; }

        public long AlbumId { get; private set; }

        private class Builder
        {
            private string _title;
            private string _description;
            private int _sequenceNumber;
            private Uri _audioUrl;
            private double _durationInSecond;
            private string _subtitles;
            private bool _isVisible;
            private long _albumId;

            #region

            public Builder Title(string title)
            {
                _title = title;
                return this;
            }

            public Builder Description(string description)
            {
                _description = description;
                return this;
            }

            public Builder SequenceNumber(int sequenceNumber)
            {
                _sequenceNumber = sequenceNumber;
                return this;
            }

            public Builder AudioUrl(Uri audioUrl)
            {
                _audioUrl = audioUrl;
                return this;
            }

            public Builder DurationInSecond(double durationInSecond)
            {
                _durationInSecond = durationInSecond;
                return this;
            }

            public Builder Subtitles(string subtitles)
            {
                _subtitles = subtitles;
                return this;
            }

            public Builder IsVisible(bool isVisible)
            {
                _isVisible = isVisible;
                return this;
            }

            public Builder AlbumId(long albumId)
            {
                _albumId = albumId;
                return this;
            }

            #endregion 链式赋值

            private Audio Build()
            {
                if (string.IsNullOrEmpty(_title))
                {
                    throw new DomainException($"{nameof(_title)}为空");
                }

                if (_sequenceNumber <= 0)
                {
                    throw new DomainException($"{nameof(_sequenceNumber)}不能小于0");
                }
                if (string.IsNullOrEmpty(_audioUrl.ToString()))
                {
                    throw new DomainException($"{nameof(_audioUrl)}为空");
                }
                if (_durationInSecond <= 0)
                {
                    throw new DomainException($"{nameof(_durationInSecond)}不能小于0");
                }
                if (string.IsNullOrEmpty(_subtitles))
                {
                    throw new DomainException($"{nameof(_subtitles)}为空");
                }

                if (_albumId == 0)
                {
                    throw new DomainException($"{nameof(_albumId)}为空");
                }

                Audio entity = new Audio()
                {
                    Id = IdGenerateHelper.Instance.GenerateId(),
                    Title = _title,
                    Description = _description,
                    SequenceNumber = _sequenceNumber,
                    AudioUrl = _audioUrl,
                    DurationInSecond = _durationInSecond,
                    Subtitles = _subtitles,
                    IsVisible = _isVisible,
                    AlbumId = _albumId,
                };

                return entity;
            }
        }
    }
}