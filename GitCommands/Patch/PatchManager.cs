        public List<Patch> Patches { get; set; } = new List<Patch>();
            ChunkList selectedChunks = ChunkList.GetSelectedChunks(text, selectionPosition, selectionLength, staged, out var header);
            ChunkList selectedChunks = ChunkList.GetSelectedChunks(text, selectionPosition, selectionLength, staged, out var header);
            return GetPatchBytes(header, body, fileContentEncoding);
            Patches = patchProcessor.CreatePatchesFromString(text).ToList();
            foreach (Patch patchApply in Patches)