﻿using System;
using System.IO;
using System.Security.Cryptography;
using EvolveDb.Metadata;
using EvolveDb.Migration;
using Xunit;
using static EvolveDb.Tests.TestContext;
using static EvolveDb.Tests.TestUtil;

namespace EvolveDb.Tests.Migration
{
    public class FileMigrationScriptTest
    {
        [Fact]
        [Category(Test.Migration)]
        public void Should_throw_NotSupportedException_when_type_is_not_migration_or_repeatable_migration()
        {
            Assert.Throws<NotSupportedException>(() => new FileMigrationScript(CrLfScriptPath, "2.3.1", "desc", MetadataType.EmptySchema));
        }

        [Fact]
        [Category(Test.Migration)]
        public void CalculateChecksum_should_not_return_null()
        {
            string checksum = BuildFileMigrationScript(CrLfScriptPath, "2.3.1").CalculateChecksum();
            Assert.False(string.IsNullOrEmpty(checksum));
        }

        [Fact]
        [Category(Test.Migration)]
        public void ValidateChecksum_should_work_with_both_crlf_and_lf_versions()
        {
            // Arrange
            var crlfScript = BuildFileMigrationScript(CrLfScriptPath, "2.3.1");
            string lfCheckSum = BuildFileMigrationScript(LfScriptPath, "2.3.2").CalculateChecksum();

            // Assert
            crlfScript.ValidateChecksum(lfCheckSum);
            Assert.NotEqual(File.ReadAllText(CrLfScriptPath), File.ReadAllText(LfScriptPath));
        }

        [Fact]
        [Category(Test.Migration)]
        public void ValidateChecksum_should_work_with_old_checksum_version()
        {
            // Arrange
            var loader = new FileMigrationLoader(new EvolveConfiguration { Locations = new[] { "Resources/LF_CRLF" } });

            // Assert
            foreach (FileMigrationScript script in loader.GetMigrations())
            {
                script.ValidateChecksum(FallbackCheck(script.Path));
            }
        }

        [Fact]
        [Category(Test.Migration)]
        public void ValidateChecksum_throws_EvolveValidationException_when_checksums_mismatch()
        {
            // Arrange
            var crlfScript = BuildFileMigrationScript(CrLfScriptPath, "2.3.1");

            // Assert
            Assert.Throws<EvolveValidationException>(() => crlfScript.ValidateChecksum("checksums mismatch"));
        }

        [Fact]
        [Category(Test.Migration)]
        public void CalculateChecksum_should_be_the_same_with_both_crlf_and_lf_versions()
        {
            // Arrange
            string crlfCheckSum = BuildFileMigrationScript(CrLfScriptPath, "2.3.1").CalculateChecksum();
            string lfCheckSum = BuildFileMigrationScript(LfScriptPath, "2.3.2").CalculateChecksum();

            // Assert
            Assert.Equal(crlfCheckSum, lfCheckSum);
            Assert.NotEqual(File.ReadAllText(CrLfScriptPath), File.ReadAllText(LfScriptPath));
        }

        [Fact]
        [Category(Test.Migration)]
        public void IsTransactionEnabled_should_be_false_when_tx_off_is_found_in_the_script()
        {
            Assert.False(BuildFileMigrationScript(PostgreSQL.VacuumScriptPath).IsTransactionEnabled);
        }

        [Fact]
        [Category(Test.Migration)]
        public void IsTransactionEnabled_should_be_true_when_tx_off_is_not_found_in_the_script()
        {
            Assert.True(BuildFileMigrationScript(SQLite.ChinookScriptPath).IsTransactionEnabled);
        }

        [Fact]
        [Category(Test.Migration)]
        public void MustRepeatAlways_should_be_true_when_repeat_always_is_found_in_the_script()
        {
            Assert.True(BuildRepeatableFileMigrationScript(SQLite.ChinookScriptPath).MustRepeatAlways);
        }

        [Fact]
        [Category(Test.Migration)]
        public void MustRepeatAlways_should_be_false_when_repeat_always_is_found_in_the_script()
        {
            Assert.False(BuildRepeatableFileMigrationScript(PostgreSQL.VacuumScriptPath).MustRepeatAlways);
        }

        /// <summary>
        ///     Calculate the checksum with the pre v1.8.0 version.
        /// </summary>
        private static string FallbackCheck(string path)
        {
            using var md5 = MD5.Create();
            using FileStream stream = File.OpenRead(path);
            byte[] checksum = md5.ComputeHash(stream);
            return BitConverter.ToString(checksum).Replace("-", string.Empty);
        }
    }
}
