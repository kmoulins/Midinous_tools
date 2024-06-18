using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace midinous_tools_avalonia.ViewModels
{
    internal class Utils
    {
        public static async Task<string> OpenTextFile()
        {
            var file = await DoOpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open Text File",
                AllowMultiple = false
            }); 
            if (file != null)
            {  // Open reading stream from the first file.
                await using var stream = await file.OpenReadAsync();
                using var streamReader = new StreamReader(stream);
                // Reads all the content of file as a text.
                return await streamReader.ReadToEndAsync();
            }
            return "";

        }

        private static async Task<IStorageFile?> DoOpenFilePickerAsync(FilePickerOpenOptions options)
        {
            // For learning purposes, we opted to directly get the reference
            // for StorageProvider APIs here inside the ViewModel. 

            // For your real-world apps, you should follow the MVVM principles
            // by making service classes and locating them with DI/IoC.

            // See IoCFileOps project for an example of how to accomplish this.
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
                desktop.MainWindow?.StorageProvider is not { } provider)
                throw new NullReferenceException("Missing StorageProvider instance.");

            var files = await provider.OpenFilePickerAsync(options);

            return files?.Count >= 1 ? files[0] : null;
        }

        public static async Task SaveTextFile(string content)
        {

            var file = await DoSaveFilePickerAsync(new FilePickerSaveOptions()
            {
                Title = "Save Text File",
                DefaultExtension = "json",
                FileTypeChoices = new[]
                {
                    new FilePickerFileType("json"){ 
                        Patterns = new[] {"*.json"},
                        MimeTypes=new[] {"application/json"},
                        AppleUniformTypeIdentifiers=new[] { "json" }

        }
                }
            });
            if (file != null)
            {  // Open reading stream from the first file.
                await using var stream = await file.OpenWriteAsync();
                using var streamWriter = new StreamWriter(stream);
                // Reads all the content of file as a text.
                await streamWriter.WriteAsync(content);
            }


        }
        private static async Task<IStorageFile?> DoSaveFilePickerAsync(FilePickerSaveOptions options)
        {
            // For learning purposes, we opted to directly get the reference
            // for StorageProvider APIs here inside the ViewModel. 

            // For your real-world apps, you should follow the MVVM principles
            // by making service classes and locating them with DI/IoC.

            // See DepInject project for a sample of how to accomplish this.
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
                desktop.MainWindow?.StorageProvider is not { } provider)
                throw new NullReferenceException("Missing StorageProvider instance.");

            return await provider.SaveFilePickerAsync(options);
        }
    }
}
