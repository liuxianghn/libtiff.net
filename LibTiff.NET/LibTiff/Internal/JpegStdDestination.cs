﻿/* Copyright (C) 2008-2009, Bit Miracle
 * http://www.bitmiracle.com
 * 
 * This software is based in part on the work of the Sam Leffler, Silicon 
 * Graphics, Inc. and contributors.
 *
 * Copyright (c) 1988-1997 Sam Leffler
 * Copyright (c) 1991-1997 Silicon Graphics, Inc.
 * For conditions of distribution and use, see the accompanying README file.
 */

using System;
using System.Collections.Generic;
using System.Text;

using BitMiracle.LibJpeg.Classic;

namespace BitMiracle.LibTiff.Internal
{
    /// <summary>
    /// JPEG library destination data manager.
    /// These routines direct compressed data from libjpeg into the
    /// libtiff output buffer.
    /// </summary>
    class JpegStdDestination : jpeg_destination_mgr
    {
        private Tiff m_tif;

        public JpegStdDestination(Tiff tif)
        {
            m_tif = tif;
        }

        public override void init_destination()
        {
            initInternalBuffer(m_tif.m_rawdata);
        }

        public override bool empty_output_buffer()
        {
            /* the entire buffer has been filled */
            m_tif.m_rawcc = m_tif.m_rawdatasize;
            m_tif.flushData1();

            initInternalBuffer(m_tif.m_rawdata);
            return true;
        }

        public override void term_destination()
        {
            m_tif.m_rawcp = m_tif.m_rawdatasize - freeInBuffer;
            m_tif.m_rawcc = m_tif.m_rawdatasize - freeInBuffer;
            /* NB: libtiff does the final buffer flush */
        }
    }
}