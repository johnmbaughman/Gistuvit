using Microsoft.Extensions.Configuration;

namespace Gistuvit.Microsoft.Extensions.Hosting;

public class SettingsManager<T>(IConfiguration configuration, CipherService cipherService)
    where T : class, new()
{
    private readonly string _appSettingsFile = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

    /// <summary>
    /// Gets the values.
    /// </summary>
    /// <value>The values.</value>
    public T Values { get; private set; } = SetConfiguration(configuration);

    /// <summary>
    /// Adds the or update application setting.
    /// </summary>
    /// <typeparam name="TV">The type of the tv.</typeparam>
    /// <param name="sectionPathKey">The section path key.</param>
    /// <param name="value">The value.</param>
    /// <param name="encrypt">if set to <c>true</c> [encrypt].</param>
    /// <exception cref="System.InvalidOperationException"></exception>
    public void AddOrUpdateAppSetting<TV>(string sectionPathKey, TV value, bool encrypt = false)
    {
        try
        {
            var json = File.ReadAllText(_appSettingsFile);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json) ?? throw new InvalidOperationException();

            SetValueRecursively(sectionPathKey, jsonObj, value, encrypt);

            var output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_appSettingsFile, output);

            Values = jsonObj[sectionPathKey.Split(":")[0]].ToObject<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error writing app settings | {0}", ex.Message);
        }
    }

    private static T SetConfiguration(IConfiguration configuration)
    {
        var values = new T();
        configuration.Bind(values, c => c.BindNonPublicProperties = true);
        return values;
    }

    private void SetValueRecursively<TV>(string sectionPathKey, dynamic jsonObj, TV value, bool encrypt)
    {
        // split the string at the first ':' character
        var remainingSections = sectionPathKey.Split(":", 2);

        var currentSection = remainingSections[0];
        if (remainingSections.Length > 1)
        {
            // continue with the process, moving down the tree
            var nextSection = remainingSections[1];
            SetValueRecursively(nextSection, jsonObj[currentSection], value, encrypt);
        }
        else
        {
            // we've got to the end of the tree, set the value
            if (encrypt && value is not null)
            {
                jsonObj[currentSection] = cipherService.Encrypt(value.ToString());
                return;
            }

            jsonObj[currentSection] = value;
        }
    }
}