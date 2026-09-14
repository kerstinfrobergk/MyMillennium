using System;
using System.Collections.Generic;
using System.Text;

namespace MyMillennium.Functions.Messages
{
    public record ProcessArtImage(
        int ArtItemId,
        string BlobItemName
    );
}


/*
 * Retrieve Service Bus message and map to ProcessArtImage.

Process ArtItem and complete Service Bus message
*/